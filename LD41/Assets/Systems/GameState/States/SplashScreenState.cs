using System;
using Systems.GameState.States.Messages;
using Systems.GameState.Time;
using Systems.UI;
using UniRx;

namespace Systems.GameState.States
{
    public class SplashScreenState : IGameState
    {
        private IDisposable _waitForStartDisposable;
        private GameControllerSystem _context;

        public void Enter(GameControllerSystem context)
        {
            _context = context;
            MessageBroker.Default.Publish(new MessageShowSplashScreen());

            _waitForStartDisposable = MessageBroker.Default.Receive<MessageStartGame>()
                .Subscribe(StartGameRecieved);
        }

        private void StartGameRecieved(MessageStartGame messageStartGame)
        {
            _context.NextState();
        }

        public void Exit()
        {
            MessageBroker.Default.Publish(new MessageTimerStart());
            _waitForStartDisposable.Dispose();
        }
    }
}