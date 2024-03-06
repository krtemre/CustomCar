using Assets.Scripts.VehicleParts.Wheel;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.VehicleParts.Gear
{
    public class GearBox : MonoBehaviour
    {
        private List<Gear> Gears;
        [SerializeField] private List<GearData> Datas;

        private Gear ReverseGear;
        [SerializeField] private GearData ReverseData;

        private Gear currentGear = null;

        private bool Connected, InReverse; //False means is in idle
        [SerializeField] private int CurrentShiftIndex; // Not included reverse

        public int Shift
        {
            get
            {
                if (!Connected) { return 0; }
                else if (InReverse) { return -1; }
                return CurrentShiftIndex + 1;
            }
        }

        [SerializeField] private Differential differential;

        private void Awake()
        {
            if (Gears == null)
            {
                Gears = new List<Gear>();
            }

            if (Datas == null)
            {
                Debug.LogError("Datas is null");
            }
            else
            {
                foreach (var data in Datas)
                {
                    Gears.Add(new Gear(data));
                }
            }

            if(ReverseData == null)
            {
                Debug.LogError("Reverse Data is null");
            }
            else
            {
                ReverseGear = new Gear(ReverseData);
            }

            Connected = true;
            CurrentShiftIndex = 0;

            if (Gears.Count > 0)
            {
                currentGear = Gears[CurrentShiftIndex];
            }
        }

        private void Start()
        {
            if (differential is null)
            {
                Debug.LogError("Differential Box is NULL");
            }
        }

        /// <summary>
        /// Changes the shift in gearbox
        /// </summary>
        /// <param name="nextShift">True means up the shift value if possible
        /// False means down the shift value till reverse</param>
        public float ChangeShift(bool nextShift, float rpm)
        {
            if (!Connected) { return rpm; } // Gearbox has no connection to engine so no change available yet

            int wantedShiftIndex = CurrentShiftIndex + (nextShift ? 1 : -1);

            if (wantedShiftIndex < 0 || wantedShiftIndex >= Gears.Count) { return rpm; } //shift index is not in range so do nothing

            if(currentGear == null)
            {
                Debug.LogError("Current Shift is NULL");
                return -1f;
            }

            float gearChangeRatio = currentGear.CogRatio / Gears[wantedShiftIndex].CogRatio;
            InReverse = false;
            CurrentShiftIndex = wantedShiftIndex;
            currentGear = Gears[CurrentShiftIndex];

            return rpm / gearChangeRatio;
        }

        /// <summary>
        /// Tries to make transaction to reverse gear.
        /// </summary>
        /// <param name="rpm">Controls rpm value for correct transaction</param>
        /// <returns>True if changded to reverse otherwise false</returns>
        public bool TryChangeToReverse(float rpm)
        {
            if (rpm > 1 || CurrentShiftIndex > 0 || !Connected)
            {
                return false;
            }
            InReverse = true;
            CurrentShiftIndex = 0;
            currentGear = ReverseGear;

            return true;
        }

        //Calculates rpm to pass differential
        public float CalculateRPM(float engineRPM)
        {
            if (!Connected) { return engineRPM; }

            if (currentGear == null)
            {
                Debug.LogError("Current Shift is NULL");
                return -1f;
            }

            engineRPM = currentGear.OutputRpm(engineRPM);

            return differential.ShaftInput(engineRPM);
        }
    }
}