using SystemBase;
using UnityEngine;

namespace Systems.Driving
{
    public class DrivingConfigComponent : GameComponent
    {
        public float AccelerationFactor;
        public float MaxSpeed;

        public float SteerFactor;
        public float SteerLerpFactor;

        public float LateralFrictionFactor;
        public float BackwardFrictionFactor;
    }
}