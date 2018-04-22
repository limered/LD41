using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Systems.Camera
{
    [GameSystem(typeof(CarSystem))]
    public class CameraSystem : GameSystem<CameraComponent>
    {
        public override void Register(CameraComponent component)
        {
            component.FixedUpdateAsObservable()
                .Select(_ => component)
                .Subscribe(UpdateCamera)
                .AddTo(IoC.Game);
        }

        private void UpdateCamera(CameraComponent cameraComponent)
        {
            cameraComponent.transform.position = Vector3.Lerp(cameraComponent.transform.position, cameraComponent.Helper.transform.position, cameraComponent.PositionLerpFactor);
            cameraComponent.transform.up = Vector3.Lerp(cameraComponent.transform.up, cameraComponent.Car.ForwardVector, cameraComponent.RotationLerpFactor);
        }
    }
}
