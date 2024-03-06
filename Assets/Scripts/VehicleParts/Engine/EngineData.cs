using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.VehicleParts.Engine
{
    [CreateAssetMenu(fileName = "EngineData", menuName = "Vehicle/EngineData")]
    public class EngineData : ScriptableObject
    {
        // V8 Engines has 8 pistons
        public int PistonCount;
        // Mean of cynlders power is 60
        public float SinglePistonPower;
        public float EnginePower
        {
            get
            {
                return PistonCount * SinglePistonPower;
            }
        }
    }
}
