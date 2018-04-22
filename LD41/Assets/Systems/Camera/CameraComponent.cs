using SystemBase;
using Systems.Driving;
using UnityEngine;

namespace Systems.Camera
{
    public class CameraComponent : GameComponent
    {
        public GameObject Helper;
        public CarComponent Car;

        public float PositionLerpFactor;
        public float RotationLerpFactor;
    }
}