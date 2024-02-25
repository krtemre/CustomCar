using UnityEngine;

namespace Assets.Scripts.VehicleParts.Gear
{
    public class Gear : MonoBehaviour
    {
        //----- Fields & Props -----
        [SerializeField] private GearData Data;
        private float cogRatio;

        //----- Methods -----
        private void Awake()
        {
            if (Data == null)
            {
                Debug.LogError("Data is missing...");
                cogRatio = 1;
            }
            else
            {
                cogRatio = Data.CogNumber / Data.CounterCogNumber;
            }

        }

        public float OutputRpm(float rpm)
        {
            return rpm / cogRatio;
        }

        public float OutputTorque(float torque)
        {
            return torque * cogRatio;
        }
    }


    [CreateAssetMenu(fileName = "GearData", menuName = "Vehicle/GearData")]
    public class GearData : ScriptableObject
    {
        private int shiftNumber;
        public int ShiftNumber
        {
            get { return shiftNumber; }
            set { shiftNumber = value; }
        }

        private int cogNumber;
        public int CogNumber
        {
            get { return cogNumber; }
            set { cogNumber = value; }
        }

        private int counterCogNumber;
        public int CounterCogNumber
        {
            get { return counterCogNumber; }
            set { counterCogNumber = value; }
        }
    }
}