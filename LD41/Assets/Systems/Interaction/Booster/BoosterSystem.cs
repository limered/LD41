using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Systems.Interaction.Booster
{
    [GameSystem(typeof(CarSystem))]
    public class BoosterSystem : GameSystem<BoostSystenConfigComponent, BoostSpawnerComponent, BoostComponent, BoostPrepareComponent, CarComponent>
    {
        private BoostSystenConfigComponent _config;
        private CarComponent _car;

        public override void Register(BoostSystenConfigComponent component)
        {
            _config = component;
        }

        public override void Register(BoostSpawnerComponent component)
        {
            throw new System.NotImplementedException();
        }

        public override void Register(BoostComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Select(coll => new Tuple<Collision2D, BoostComponent>(coll, component))
                .Subscribe(OnCarCollision)
                .AddTo(component);
        }

        private void OnCarCollision(Tuple<Collision2D, BoostComponent> tuple)
        {
            _car.Velocity = _car.Velocity * tuple.Item2.BoosterStrength;
        }

        public override void Register(BoostPrepareComponent component)
        {
            component.OnCollisionStay2DAsObservable()
                .Select(coll => new Tuple<Collision2D, BoostPrepareComponent>(coll, component))
                .Subscribe(OnCarOverLane)
                .AddTo(component);
        }

        private void OnCarOverLane(Tuple<Collision2D, BoostPrepareComponent> tuple)
        {
            
        }

        public override void Register(CarComponent component)
        {
            _car = component;
        }
    }

    public class BoostPrepareComponent : GameComponent
    {
        public string Name;
    }

    public class BoostSpawnerComponent : GameComponent
    {
        public string Name;
    }
}
