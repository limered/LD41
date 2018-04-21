using System;
using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;
using Utils.Math;
using Utils.Unity;

namespace Systems.Driving
{
    [GameSystem]
    public class CarSystem : GameSystem<DrivingConfigComponent, CarComponent>
    {
        private DrivingConfigComponent _config;

        public override void Register(CarComponent component)
        {
            component.FixedUpdateAsObservable()
                .Select(_ => component)
                .Subscribe(CarOnUpdate)
                .AddTo(IoC.Game);
        }

        private void CarOnUpdate(CarComponent carComponent)
        {
            HandleInput(carComponent);
            ApplyFriction(carComponent);
            Animate(carComponent);
            ApplyAnimationToModel(carComponent);
        }

        private void Animate(CarComponent carComponent)
        {
            var futureVelocity = carComponent.Velocity + carComponent.Acceleration * Time.fixedDeltaTime;
            var speed = futureVelocity.magnitude;
            carComponent.SteerAngle *= speed / _config.MaxSpeed;
            if (speed < _config.MaxSpeed)
            {
                carComponent.Velocity = futureVelocity;
            }

            var newForward = carComponent.ForwardVector.Rotate(carComponent.SteerAngle);
            var steerAmount = _config.SteerLerpFactor * Time.fixedDeltaTime;
            carComponent.ForwardVector = Vector2.Lerp(carComponent.ForwardVector, newForward, steerAmount).normalized;
        }

        private static void ApplyAnimationToModel(CarComponent carComponent)
        {
            var positionChange = carComponent.Velocity * Time.fixedDeltaTime;
            carComponent.transform.position = new Vector3(carComponent.transform.position.x + positionChange.x,
                carComponent.transform.position.y + positionChange.y,
                carComponent.transform.position.z);

            carComponent.transform.up = carComponent.ForwardVector;
            
        }

        private void ApplyFriction(CarComponent carComponent)
        {
            var right = carComponent.ForwardVector.Rotate(-90);
            var lateralVel = right * Vector2.Dot(carComponent.Velocity, right);
            var latFriction = lateralVel * -_config.LateralFrictionFactor;
            var backFricktion = carComponent.Velocity * -_config.BackwardFrictionFactor;
            carComponent.Velocity = carComponent.Velocity + (backFricktion + latFriction) * Time.fixedDeltaTime;
        }

        private void HandleInput(CarComponent carComponent)
        {
            if (KeyCode.W.IsPressed() || KeyCode.UpArrow.IsPressed())
            {
                carComponent.Acceleration = carComponent.ForwardVector * _config.AccelerationFactor;
            }
            else
            {
                carComponent.Acceleration = Vector2.zero;
            }
            var steerInput = 0;
            if (KeyCode.A.IsPressed() || KeyCode.LeftArrow.IsPressed())
            {
                steerInput += 1;
            }
            if (KeyCode.D.IsPressed() || KeyCode.RightArrow.IsPressed())
            {
                steerInput -= 1;
            }
            carComponent.SteerAngle = steerInput * _config.SteerFactor;
            
        }


        public override void Register(DrivingConfigComponent component)
        {
            _config = component;
        }

        private enum SteerDir { None, Left, Right }
    }
    
}
