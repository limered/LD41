using UnityEngine;

namespace Systems.GameState.TaskGenerator
{
    public class MultiplyAbstractTaskGeneratorImpl : AbstractTaskGenerator
    {
        public override Task Generate(int min, int max)
        {
            var task = new Task
            {
                FirstNumber = Random.Range(min, max),
                SecondNumber = Random.Range(min, max)
            };
            task.Result = task.FirstNumber * task.SecondNumber;
            task.Operation = "x";

            GenerateWrongs(task);

            return task;
        }
    }
}