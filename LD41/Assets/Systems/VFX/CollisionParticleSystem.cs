using System;
using SystemBase;
using Systems.Driving;
using Systems.VFX.Messages;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Systems.VFX
{

    [GameSystem(typeof(CarSystem))]
    public class CollisionParticleSystem : GameSystem<CollisionParticleConfigComponen, WallPArticleComponent, ObstacleParticleComponent>
    {
        private CollisionParticleConfigComponen _config;

        public override void Register(CollisionParticleConfigComponen component)
        {
            _config = component;

            MessageBroker.Default.Receive<MessageWallParticle>()
                .Subscribe(SpawnWallParticles)
                .AddTo(IoC.Game);
        }

        private void SpawnWallParticles(MessageWallParticle messageWallParticle)
        {
            var pos = new Vector3(messageWallParticle.Position.x, messageWallParticle.Position.y, -2);
            GameObject.Instantiate(_config.WallPArticleSystemPrefab, pos,
                Quaternion.AngleAxis(-90, new Vector3(0, messageWallParticle.Velocity.x, messageWallParticle.Velocity.y)));
        }

        public override void Register(WallPArticleComponent component)
        {
            component.FixedUpdateAsObservable()
                .Select(_ => component)
                .Subscribe(UpdateParticles)
                .AddTo(component);
        }

        private void UpdateParticles(WallPArticleComponent wallPArticleComponent)
        {
            
        }

        public override void Register(ObstacleParticleComponent component)
        {
            
        }
    }

    public class ObstacleParticleComponent : GameComponent
    {
    }

    public class WallPArticleComponent : GameComponent
    {
    }
}
