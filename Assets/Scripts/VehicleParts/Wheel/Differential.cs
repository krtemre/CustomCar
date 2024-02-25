using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.VehicleParts.Wheel
{
    public class Differential : MonoBehaviour
    {
        //----- Fields & Props -----
        [SerializeField] private int InputCogTeethNumber = 10;
        [SerializeField] private int OutputCogTeethNumber = 38;

        private float LastWheelRatio;

        [SerializeField] private List<Wheel> Wheels;
        //----- Methods -----
        private void Awake()
        {
            if(InputCogTeethNumber <= 0)
            {
                InputCogTeethNumber = 10;
            }
            if (OutputCogTeethNumber <= 0)
            {
                OutputCogTeethNumber = 38;
            }

            LastWheelRatio = OutputCogTeethNumber / InputCogTeethNumber;

            if(Wheels == null)
            {
                Debug.LogError("Wheels does not assigned...");
                Wheels = new List<Wheel>();
            }
        }

        public void ShaftInput(float RPM)
        {
            float outputRPM = RPM / LastWheelRatio;

            foreach (var wheel in Wheels)
            {
                wheel.DifferentialInput(outputRPM);
            }
        }
    }
}
