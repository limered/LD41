using SystemBase;
using UnityEngine;

namespace Systems.Driving
{
    public class DrivingConfigComponent : GameComponent
    {
        [Range(10, 100)]
        public float AccelerationFactor;
        public float MaxSpeed;

        public float MaxCurveEnergy;
        public float SteerFactor;
        public float SteerLerpFactor;

        public float LateralFrictionFactor;
        public float BackwardFrictionFactor;
    }
}