using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Interaction.Booster
{
    [GameSystem(typeof(CarSystem))]
    public class BoosterSystem : GameSystem<BoostSystenConfigComponent, BoostSpawnerComponent, BoostComponent, BoostPrepareComponent, CarComponent>
    {
        private CarComponent _car;
        private BoostSystenConfigComponent _config;
        public override void Register(BoostSystenConfigComponent component)
        {
            _config = component;

            //Observable.Interval(TimeSpan.FromSeconds(10))
            //    .Subscribe(l => MessageBroker.Default.Publish(new MessageSpawnTask("test")));
        }

        public override void Register(BoostSpawnerComponent component)
        {
            MessageBroker.Default.Receive<MessageSpawnTask>()
                .Select(m => new Tuple<MessageSpawnTask, BoostSpawnerComponent>(m, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Subscribe(SpawnBooster)
                .AddTo(component);
        }

        public override void Register(BoostComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Select(coll => new Tuple<Collision2D, BoostComponent>(coll, component))
                .Subscribe(OnCarCollision)
                .AddTo(component);

            MessageBroker.Default.Receive<MessageDespawnTask>()
                .Select(m => new Tuple<MessageDespawnTask, BoostComponent>(m, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Delay(TimeSpan.FromMilliseconds(500))
                .Subscribe(tuple => Object.Destroy(tuple.Item2.gameObject))
                .AddTo(component);
        }
        public override void Register(BoostPrepareComponent component)
        {
            component.OnCollisionStay2DAsObservable()
                .Select(coll => new Tuple<Collision2D, BoostPrepareComponent>(coll, component))
                .Subscribe(OnCarOverLane)
                .AddTo(component);
        }
        public override void Register(CarComponent component)
        {
            _car = component;
        }
        private void OnCarCollision(Tuple<Collision2D, BoostComponent> tuple)
        {
            _car.Velocity = _car.Velocity * tuple.Item2.BoosterStrength;

            MessageBroker.Default.Publish(new MessageDespawnTask(tuple.Item2.Name));
        }
        private void OnCarOverLane(Tuple<Collision2D, BoostPrepareComponent> tuple)
        {
        }
        private void SpawnBooster(Tuple<MessageSpawnTask, BoostSpawnerComponent> tuple)
        {
            var booster = Object.Instantiate(_config.BoosterPrefab, tuple.Item2.transform).GetComponent<BoostComponent>();

            booster.BoosterStrength = _config.MinStrength;
            booster.Name = tuple.Item2.Name;
        }
    }

    public class BoostPrepareComponent : GameComponent
    {
        public float DistanceTravelled;
        public string Name;
    }
}