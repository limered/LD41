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

    public class CollisionParticleConfigComponen : GameComponent
    {
        public ParticleSystem WallPArticleSystemPrefab;
        public ParticleSystem ObstacleParticleSystemPrefab;
    }
}
