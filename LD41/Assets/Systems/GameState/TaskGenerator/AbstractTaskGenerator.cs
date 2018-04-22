using Systems.GameState.States;
using UnityEngine;

namespace Systems.GameState.TaskGenerator
{
    public abstract class AbstractTaskGenerator : ITaskGenerator
    {
        public abstract Task Generate();

        protected static void GenerateWrongs(Task task)
        {
            do
            {
                task.Wrong1 = task.Result + Random.Range(-10, 10);
            } while (task.Wrong1 == task.Result);
            do
            {
                task.Wrong2 = task.Result + Random.Range(-10, 10);
            } while (task.Wrong2 == task.Result || task.Wrong1 == task.Wrong2);
        }

        protected static TrackPosition GetResultPosition()
        {
            return (TrackPosition) Random.Range(0, 3);
        }
    }
}