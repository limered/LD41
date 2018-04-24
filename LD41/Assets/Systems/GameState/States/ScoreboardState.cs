using System;
using Systems.GameState.States.Messages;
using UniRx;

namespace Systems.GameState.States
{
    public class ScoreboardState : IGameState
    {
        private IDisposable _restartDisposable;

        public void Enter(GameControllerSystem context)
        {
            MessageBroker.Default.Publish(new MessegeEndGame());

            _restartDisposable = MessageBroker.Default.Receive<MessageRestartGame>()
                .Select(m => context)
                .Subscribe(OnRestartGame);
        }

        private void OnRestartGame(GameControllerSystem context)
        {
            context.NextState();
        }

        public void Exit()
        {
            _restartDisposable.Dispose();
        }
    }
}