using System.Linq;
using SystemBase;
using Systems.Driving;
using Systems.VFX.Messages;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils.Math;

namespace Systems.Interaction.Walls
{
    [GameSystem(typeof(CarSystem))]
    public class WallSystem : GameSystem<CarComponent, WallComponent>
    {
        private CarComponent _car;
        public override void Register(WallComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Subscribe(CarCollision)
                .AddTo(component);
        }

        private void CarCollision(Collision2D collision2D)
        {
            var center = collision2D.contacts.Select(c => c.point).Aggregate((one, two) => one + two);
            center = new Vector2(center.x/collision2D.contacts.Length, center.y / collision2D.contacts.Length);
            var dir = _car.transform.position.DirectionTo(center);
            _car.Velocity = new Vector2(-dir.x * 25, -dir.y * 25);

            _car.WallCrashSound.pitch = Random.Range(0.7f, 1.3f);
            _car.WallCrashSound.Play();

            MessageBroker.Default.Publish(new MessageWallParticle{Position = center, Forward = _car.ForwardVector});
        }

        public override void Register(CarComponent component)
        {
            _car = component;
        }
    }
}
