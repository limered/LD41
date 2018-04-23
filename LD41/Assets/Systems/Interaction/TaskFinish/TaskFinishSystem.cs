using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems.Interaction.TaskFinish
{
    [GameSystem]
    public class TaskFinishSystem : GameSystem<TaskFinishConfigComponent, TaskFinishSpawnerComponent, ObstacleComponent, BoostComponent, CarComponent>
    {
        private CarComponent _car;
        private TaskFinishConfigComponent _config;
        public override void Register(TaskFinishSpawnerComponent component)
        {
            MessageBroker.Default.Receive<MessageSpawnTask>()
                .Select(message => new Tuple<MessageSpawnTask, TaskFinishSpawnerComponent>(message, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Subscribe(SpawnObject)
                .AddTo(component);
        }

        public override void Register(ObstacleComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Select(coll => new Tuple<Collision2D, ObstacleComponent>(coll, component))
                .Subscribe(OnObstacleCollision)
                .AddTo(component);

            MessageBroker.Default.Receive<MessageDespawnTask>()
                .Select(m => new Tuple<MessageDespawnTask, ObstacleComponent>(m, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Delay(TimeSpan.FromMilliseconds(500))
                .Subscribe(tuple => Object.Destroy(tuple.Item2.gameObject))
                .AddTo(component);
        }
        public override void Register(BoostComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Select(coll => new Tuple<Collision2D, BoostComponent>(coll, component))
                .Subscribe(OnCarBoost)
                .AddTo(component);

            MessageBroker.Default.Receive<MessageDespawnTask>()
                .Select(m => new Tuple<MessageDespawnTask, BoostComponent>(m, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Delay(TimeSpan.FromMilliseconds(500))
                .Subscribe(tuple => Object.Destroy(tuple.Item2.gameObject))
                .AddTo(component);
        }
        public override void Register(TaskFinishConfigComponent component)
        {
            _config = component;
        }
        public override void Register(CarComponent component)
        {
            _car = component;
        }
        private void OnCarBoost(Tuple<Collision2D, BoostComponent> tuple)
        {
            _car.Velocity = _car.Velocity * tuple.Item2.BoosterStrength;

            MessageBroker.Default.Publish(new MessageDespawnTask(tuple.Item2.Name));
        }
        private void OnObstacleCollision(Tuple<Collision2D, ObstacleComponent> tuple)
        {
            _car.Velocity = _car.Velocity * -_config.ObstacleStrength;

            _car.ObstacleCrashSound.pitch = Random.Range(0.7f, 1.3f);
            _car.ObstacleCrashSound.Play();

            MessageBroker.Default.Publish(new MessageDespawnTask(tuple.Item2.Name));
        }
        private void SpawnObject(Tuple<MessageSpawnTask, TaskFinishSpawnerComponent> tuple)
        {
            if (tuple.Item2.TrackPosition == tuple.Item1.Task.PositionOfResult)
            {
                var booster = Object.Instantiate(_config.BoostPrefab, tuple.Item2.transform)
                    .GetComponent<BoostComponent>();

                booster.BoosterStrength = _config.BoosterStrength;
                booster.Name = tuple.Item2.Name;
            }
            else
            {
                var obstacle = Object.Instantiate(_config.ObstaclePrefab, tuple.Item2.transform);
                obstacle.GetComponent<ObstacleComponent>().Name = tuple.Item2.Name;
            }
        }
    }
}
