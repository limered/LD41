using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Interaction.Obstacles
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

            MessageBroker.Default.Receive<MessageDespawnTask>()
                .Select(m => new Tuple<MessageDespawnTask, ObstacleComponent>(m, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.ParentName))
                .Delay(TimeSpan.FromMilliseconds(500))
                .Subscribe(tuple => Object.Destroy(tuple.Item2.gameObject))
                .AddTo(component);
        }

        public override void Register(ObstacleConfigComponent component)
        {
            _config = component;

            //Observable.Interval(TimeSpan.FromSeconds(10))
            //    .Subscribe(l => MessageBroker.Default.Publish(new MessageSpawnTask("test")));
        }
        public override void Register(ObstacleSpawnerComponent component)
        {
            MessageBroker.Default.Receive<MessageSpawnTask>()
                .Select(message => new Tuple<MessageSpawnTask, ObstacleSpawnerComponent>(message, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Subscribe(SpawnObstacleOnSelf)
                .AddTo(component);
        }
        private void OnCarCollision(Tuple<Collision2D, ObstacleComponent> tuple)
        {
            _car.Velocity = _car.Velocity * -0.4f;

            MessageBroker.Default.Publish(new MessageDespawnTask(tuple.Item2.ParentName));
        }
        private void SpawnObstacleOnSelf(Tuple<MessageSpawnTask, ObstacleSpawnerComponent> tuple)
        {
            var obstacle = Object.Instantiate(_config.OpstaclePrefab, tuple.Item2.transform);
            obstacle.GetComponent<ObstacleComponent>().ParentName = tuple.Item2.Name;
        }
    }
}