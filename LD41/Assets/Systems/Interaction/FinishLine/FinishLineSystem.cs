using System;
using SystemBase;
using Systems.Driving;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Systems.Interaction.FinishLine
{
    [GameSystem]
    public class FinishLineSystem : GameSystem<FinishLineComponent>
    {
        public override void Register(FinishLineComponent component)
        {
            component.OnCollisionEnter2DAsObservable()
                .Subscribe(CarPassedFinish)
                .AddTo(component);
        }

        private void CarPassedFinish(Collision2D collision2D)
        {
            MessageBroker.Default.Publish(new MessageFinishLine());
        }
    }
}
