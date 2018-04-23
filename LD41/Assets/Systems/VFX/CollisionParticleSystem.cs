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
    public class CollisionParticleSystem : GameSystem<CollisionParticleConfigComponen>
    {
        private CollisionParticleConfigComponen _config;

        public override void Register(CollisionParticleConfigComponen component)
        {
            _config = component;

            MessageBroker.Default.Receive<MessageWallParticle>()
                .Subscribe(SpawnWallParticles)
                .AddTo(IoC.Game);

            MessageBroker.Default.Receive<MessageObstacleParticle>()
                .Subscribe(SpawnObstacleParticles)
                .AddTo(IoC.Game);
        }

        private void SpawnObstacleParticles(MessageObstacleParticle messageObstacleParticle)
        {
            var pos = new Vector3(messageObstacleParticle.Position.x, messageObstacleParticle.Position.y, -2);
            GameObject.Instantiate(_config.ObstacleParticleSystemPrefab, pos,
                Quaternion.AngleAxis(90, new Vector3(-messageObstacleParticle.Forward.y, messageObstacleParticle.Forward.x, 0)));
        }

        private void SpawnWallParticles(MessageWallParticle messageWallParticle)
        {
            var pos = new Vector3(messageWallParticle.Position.x, messageWallParticle.Position.y, -2);
            GameObject.Instantiate(_config.WallPArticleSystemPrefab, pos,
                Quaternion.AngleAxis(90, new Vector3(-messageWallParticle.Forward.y, messageWallParticle.Forward.x, 0)));
        }
    }

    public class ObstacleParticleComponent : GameComponent
    {
    }

    public class WallPArticleComponent : GameComponent
    {
    }
}
