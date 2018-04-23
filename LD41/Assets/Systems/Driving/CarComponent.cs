using SystemBase;
using UnityEngine;

namespace Systems.Driving
{
    public class CarComponent : GameComponent
    {
        public Vector2 Acceleration;
        public Vector2 Velocity;

        public float SteerAngle = 0;
        public Vector2 ForwardVector = Vector2.up;
        public Vector2 LateralVel;

        public AudioSource WallCrashSound;
        public AudioSource ObstacleCrashSound;

        public GameObject ColliderObject;
    }
}