namespace Systems.GameState.States
{
    public interface IGameState
    {
        void Enter(GameControllerSystem context);
        void Exit();
    }

    public class ScoreboardState : IGameState
    {
        public void Enter(GameControllerSystem context)
        {
        }

        public void Exit()
        {
        }
    }
}