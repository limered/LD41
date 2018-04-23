using System;
using System.Collections.Generic;
using SystemBase;
using Systems.GameState.States;
using Systems.GameState.TaskGenerator;
using Systems.GameState.Time;
using UniRx;

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

            _states[_currentStateP].Enter(this);
        }

        private void SetupStates()
        {
            _states = new List<IGameState>();
            _states.Add(new SplashScreenState());
            _states.Add(new WaitForFinishState{ActionOnExit = ()=>MessageBroker.Default.Publish(new MessageTimerStart())});
            _states.Add(new TaskState
            {
                TaskName = "one",
                TaskGenerators = new ITaskGenerator[]
                {
                    new MultiplyTaskGeneratorImpl(1,1),
                }
            });
            _states.Add(new TaskState
            {
                TaskName = "two",
                TaskGenerators = new ITaskGenerator[]
                {
                    new AdditionAbstractTaskGenerator(10, 50),
                    new SubtractionAbstractTaskGenerator(10, 50),
                },
            });
            _states.Add(new WaitForFinishState());
            _states.Add(new TaskState
            {
                TaskName = "one",
                TaskGenerators = new ITaskGenerator[]
                {
                    new MultiplyTaskGeneratorImpl(0, 5),
                    new DivisionAbstractTaskGeneratorImpl(2, 15),
                },
            });
            _states.Add(new TaskState
            {
                TaskName = "two",
                TaskGenerators = new ITaskGenerator[]
                {
                    new MultiplyTaskGeneratorImpl(0, 20),
                    new DivisionAbstractTaskGeneratorImpl(3, 10),
                },
            });
            _states.Add(new WaitForFinishState());
            _states.Add(new TaskState
            {
                TaskName = "one",
                TaskGenerators = new ITaskGenerator[] {
                    new MultiplyTaskGeneratorImpl(0, 5),
                    new DivisionAbstractTaskGeneratorImpl(1, 5),
                    new AdditionAbstractTaskGenerator(0,10),
                    new SubtractionAbstractTaskGenerator(1,10),
                },
            });
            _states.Add(new TaskState
            {
                TaskName = "two",
                TaskGenerators = new ITaskGenerator[]
                {
                    new MultiplyTaskGeneratorImpl(1, 15),
                    new DivisionAbstractTaskGeneratorImpl(1, 5),
                    new AdditionAbstractTaskGenerator(10, 100),
                    new SubtractionAbstractTaskGenerator(50, 100),
                },
            });
            _states.Add(new WaitForFinishState{ActionOnExit = ()=>MessageBroker.Default.Publish(new MessageTimerStop())});
        }

        public void NextState()
        {
            if (_states[_currentStateP] == null) return;

            _states[_currentStateP].Exit();
            _currentStateP = (_currentStateP + 1) % _states.Count;
            _states[_currentStateP].Enter(this);
        }
    }
}
