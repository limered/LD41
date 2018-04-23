using System;
using Systems.Interaction.FinishLine;
using UniRx;

namespace Systems.GameState.States
{
    public class WaitForFinishState : IGameState
    {
        private IDisposable _disposable;

        public Action ActionOnExit;

        public void Enter(GameControllerSystem context)
        {
            _disposable = MessageBroker.Default.Receive<MessageFinishLine>()
                .Select(_ => context)
                .Subscribe(c => c.NextState());
        }

        public void Exit()
        {
            if (ActionOnExit != null)
            {
                ActionOnExit();
            }
            _disposable.Dispose();
        }
    }
}