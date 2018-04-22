using System.Collections.Generic;
using SystemBase;
using Systems.GameState.States;

namespace Systems.GameState
{
    [GameSystem]
    public class GameControllerSystem : GameSystem<GameConfigComponent>
    {
        private GameConfigComponent _config;
        private int _currentStateP;
        private List<IGameState> _states;

        public override void Register(GameConfigComponent component)
        {
            _config = component;

            SetupStates();
        }

        private void SetupStates()
        {
            _states = new List<IGameState>();
            _states.Add(new SplashScreenState());
        }

        public void NextState()
        {
            if (_states[_currentStateP] == null) return;

            _states[_currentStateP].Exit();
            _currentStateP = _currentStateP >= _states.Count ? 0 : _currentStateP+1;
            _states[_currentStateP].Enter(this);
        }
    }
}
