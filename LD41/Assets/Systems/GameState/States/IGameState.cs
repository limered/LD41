namespace Systems.GameState.States
{
    public interface IGameState
    {
        void Enter(GameControllerSystem context);
        void Exit();
    }
}