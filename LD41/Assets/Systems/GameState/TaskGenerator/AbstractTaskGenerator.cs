using UnityEngine;

namespace Systems.GameState.TaskGenerator
{
    public abstract class AbstractTaskGenerator : ITaskGenerator
    {
        public abstract Task Generate(int min, int max);

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
    }
}