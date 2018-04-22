using System;
using Systems.GameState.TaskGenerator;
using Systems.Interaction;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.GameState.States
{
    public class TaskState : IGameState
    {
        private GameControllerSystem _context;
        private Task _task;
        private IDisposable _endMessage;

        public string TaskName;
        public ITaskGenerator[] TaskGenerators;
        public int Min;
        public int Max;

        public void Enter(GameControllerSystem context)
        {
            _context = context;
            var tNr = Random.Range(0, TaskGenerators.Length);
            var gnrtr = TaskGenerators[tNr];
            _task = gnrtr.Generate();

            Debug.Log(_task.FirstNumber + _task.Operation + _task.SecondNumber + "=" + _task.Result);

            MessageBroker.Default.Publish(new MessageSpawnTask(TaskName, _task));
            _endMessage = MessageBroker.Default.Receive<MessageDespawnTask>()
                .Where(task => task.Name.Equals(TaskName))
                .Subscribe(OnBoosterHit);
        }

        private void OnBoosterHit(MessageDespawnTask messageDespawnTask)
        {
            _context.NextState();
        }

        public void Exit()
        {
            _endMessage.Dispose();
        }
    }
}