using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.VehicleParts.Wheel
{
    public class Differential : MonoBehaviour
    {
        //----- Fields & Props -----
        private Gear.Gear LastGear;
        [SerializeField] Gear.GearData LastGearData;
        [SerializeField] private List<Wheel> Wheels;

        public float SpeedInKMH;
        [SerializeField] private Speedometer speedoMeter;
        //----- Methods -----
        private void Awake()
        {
            if (LastGearData == null)
            {
                Debug.LogError("Last Gear Data is NULL");
            }
            else
            {
                LastGear = new Gear.Gear(LastGearData);
            }

            if (Wheels == null)
            {
                Debug.LogError("Wheels does not assigned...");
                Wheels = new List<Wheel>();
            }
        }

        public float ShaftInput(float RPM)
        {
            if (Wheels.Count <= 0)
            {
                Debug.LogWarning("No Wheel exists.");
                return -1f;
            }

            float outputRPM = LastGear.OutputRpm(RPM);

            foreach (var wheel in Wheels)
            {
                wheel.DifferentialInput(outputRPM);
            }

            SpeedInKMH = Wheels[0].SpeedInKMH;

            speedoMeter.UpdateSpeedometer(SpeedInKMH / 300);
            speedoMeter.TryUpdateDigitalSpeed((int)SpeedInKMH);

            return SpeedInKMH;
        }
    }
}
