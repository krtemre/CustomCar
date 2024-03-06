using Assets.Scripts.VehicleParts.Gear;
using UnityEngine;

namespace Assets.Scripts.VehicleParts.Engine
{
    public class Engine : MonoBehaviour
    {
        //----- Fields & Props -----
        [SerializeField] private EngineData Data;
        [SerializeField] private const float MAX_ENGINE_RPM = 5500;

        public float EngineRPM;

        private float EnginePower;

        [SerializeField] private GearBox gearBox;
        [SerializeField] private Speedometer rpmMeter;

        // TODO
        [SerializeField] private float IdleEngineRPM = 1000f;
        [HideInInspector] public bool GearBoxConnection;

        public float SpeedInKMH;

        //-----  Methods -----
        private void Awake()
        {
            EngineRPM = 0f;
            EnginePower = 0f;
            GearBoxConnection = true;

            if (Data == null)
            {
                Debug.LogWarning("Engine Data is NULL");
            }
            else
            {
                EnginePower = Data.EnginePower * Time.fixedDeltaTime;
            }
        }

        private void Start()
        {
            if (gearBox is null)
            {
                Debug.LogError("Gear Box is NULL");
            }
        }

        public float EngineInput(InputTypesEnum gasInput)
        {
            EngineRPM = Mathf.Max(0, Mathf.Min(MAX_ENGINE_RPM, EngineRPM + ((sbyte)gasInput * EnginePower)));
            RunEngine();

            return SpeedInKMH;
        }

        public void RunEngine()
        {
            if (EngineRPM >= 3000)
            {
                EngineRPM = gearBox.ChangeShift(true, EngineRPM);
            }

            SpeedInKMH = gearBox.CalculateRPM(EngineRPM);

            rpmMeter.UpdateSpeedometer(EngineRPM / MAX_ENGINE_RPM);
            rpmMeter.TryUpdateShift(gearBox.Shift);
        }
    }

    public enum InputTypesEnum : sbyte
    {
        None = 0,
        Acceleration = 1,
        Deacceleration = -1,
    }
}

