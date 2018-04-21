using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Systems.Interaction
{
    [GameSystem(typeof(CarSystem))]
    public class ObstacleSystem : GameSystem<ObstacleConfigComponent, ObstacleSpawnerComponent, CarComponent, ObstacleComponent>
    {
        private CarComponent _car;
        private ObstacleConfigComponent _config;
        public override void Register(CarComponent component)
        {
            _car = component;
        }

        public override void Register(ObstacleComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Select(coll => new Tuple<Collision2D, ObstacleComponent>(coll, component))
                .Subscribe(OnCarCollision)
                .AddTo(component);
        }

        public override void Register(ObstacleConfigComponent component)
        {
            _config = component;
        }
        public override void Register(ObstacleSpawnerComponent component)
        {
            MessageBroker.Default.Receive<SpawnOpstacleMessage>()
                .Select(message => new Tuple<SpawnOpstacleMessage, ObstacleSpawnerComponent>(message, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Subscribe(SpawnObstacleOnSelf)
                .AddTo(component);
        }
        private void OnCarCollision(Tuple<Collision2D, ObstacleComponent> tuple)
        {
            _car.Velocity = _car.Velocity * -0.4f;

            // destroy obstacles
        }
        private void SpawnObstacleOnSelf(Tuple<SpawnOpstacleMessage, ObstacleSpawnerComponent> tuple)
        {
            GameObject.Instantiate(_config.OpstaclePrefab, tuple.Item2.transform);
        }
    }
}