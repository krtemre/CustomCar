using UnityEngine;

namespace Assets.Scripts.VehicleParts.Wheel
{
    public class Wheel : MonoBehaviour
    {
        //----- Fields & Props -----
        private float RPS, rotateInAngle;

        [SerializeField] private float WheelCircumferenceInMeters = 150;

        public float SpeedInMS { get; private set; }
        public float SpeedInKMH
        {
            get
            {
                return SpeedInMS * 3.6f;
            }
        }

        //----- Methods -----
        private void Awake()
        {
            RPS = 0;
            SpeedInMS = 0;
        }

        private void Update()
        {
            if(SpeedInMS > 0)
            {
                this.transform.Rotate(new Vector3(Time.deltaTime * rotateInAngle, 0f, 0f));
            }
        }

        public void DifferentialInput(float RPM)
        {
            RPS = RPM / 60;
            SpeedInMS = WheelCircumferenceInMeters * RPS;
            rotateInAngle = RPS * 360;
        }
    }
}
