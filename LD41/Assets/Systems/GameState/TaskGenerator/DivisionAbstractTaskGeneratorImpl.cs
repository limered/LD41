using UnityEngine;

namespace Systems.GameState.TaskGenerator
{
    public class DivisionAbstractTaskGeneratorImpl : AbstractTaskGenerator
    {
        public override Task Generate(int min, int max)
        {
            var task = new Task
            {
                Result = Random.Range(min, max),
                SecondNumber = Random.Range(min, max)
            };
            task.FirstNumber = task.Result * task.SecondNumber;
            task.Operation = "/";

            GenerateWrongs(task);

            return task;
        }
    }
}