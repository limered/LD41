using System;
using SystemBase;
using Systems.Driving;
using Systems.GameState.States.Messages;
using UniRx;
using UnityEngine;

namespace Systems.GameState.CarSpawner
{
    [GameSystem(typeof(CarSystem))]
    public class CarSpawningSystem : GameSystem<CarComponent, CarSpawnerComponent>
    {
        private CarComponent _car;
        public override void Register(CarComponent component)
        {
            _car = component;
        }

        public override void Register(CarSpawnerComponent component)
        {
            MessageBroker.Default.Receive<MessageStartGame>()
                .Select(m => component)
                .Subscribe(SpawnCar);

        }

        private void SpawnCar(CarSpawnerComponent carSpawnerComponent)
        {
            _car.transform.position = carSpawnerComponent.transform.position;
            _car.Velocity = Vector2.zero;
            _car.Acceleration = Vector2.zero;
            _car.ForwardVector = Vector2.up;
            _car.SteerAngle = 0;
        }
    }
}
