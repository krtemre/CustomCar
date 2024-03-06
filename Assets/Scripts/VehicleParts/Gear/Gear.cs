using UnityEngine;

namespace Assets.Scripts.VehicleParts.Gear
{
    public class Gear
    {
        //----- Fields & Props -----
        public GearData Data;
        public float CogRatio;

        //----- Methods -----

        public Gear(GearData data)
        {
            Data = data;

            if (Data == null)
            {
                Debug.LogError("Data is missing...");
                CogRatio = 1;
            }
            else
            {
                CogRatio = (float)Data.CogNumber / Data.CounterCogNumber;
            }
        }

        public float OutputRpm(float rpm)
        {
            return rpm / CogRatio;
        }

        public float OutputTorque(float torque)
        {
            return torque * CogRatio;
        }
    }


    [CreateAssetMenu(fileName = "GearData", menuName = "Vehicle/GearData")]
    public class GearData : ScriptableObject
    {
        [SerializeField] private int shiftNumber;
        public int ShiftNumber
        {
            get { return shiftNumber; }
            set { shiftNumber = value; }
        }

        [SerializeField] private int cogNumber;
        public int CogNumber
        {
            get { return cogNumber; }
            set { cogNumber = value; }
        }

        [SerializeField] private int counterCogNumber;
        public int CounterCogNumber
        {
            get { return counterCogNumber; }
            set { counterCogNumber = value; }
        }
    }
}