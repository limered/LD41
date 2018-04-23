using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Systems.SFX
{
    [GameSystem(typeof(CarSystem))]
    public class CarSoundSystem : GameSystem<CarComponent, DrivingConfigComponent>
    {
        private AudioSource _sound;
        private DrivingConfigComponent _driveConfig;

        public override void Register(CarComponent component)
        {
            _sound = component.GetComponent<AudioSource>();
            component.FixedUpdateAsObservable()
                .Select(_ => component)
                .Subscribe(OnUpdate);
        }

        private void OnUpdate(CarComponent carComponent)
        {
            _sound.pitch = 1f + (carComponent.Velocity.magnitude / _driveConfig.MaxSpeed)*9;
        }

        public override void Register(DrivingConfigComponent component)
        {
            _driveConfig = component;
        }
    }
}
