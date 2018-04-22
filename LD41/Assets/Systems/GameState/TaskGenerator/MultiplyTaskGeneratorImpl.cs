using UnityEngine;

namespace Systems.GameState.TaskGenerator
{
    public class MultiplyTaskGeneratorImpl : AbstractTaskGenerator
    {
        private readonly int _min;
        private readonly int _max;

        public MultiplyTaskGeneratorImpl(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override Task Generate()
        {
            var task = new Task
            {
                FirstNumber = Random.Range(_min, _max),
                SecondNumber = Random.Range(_min, _max)
            };
            task.Result = task.FirstNumber * task.SecondNumber;
            task.Operation = "x";

            GenerateWrongs(task);

            task.PositionOfResult = GetResultPosition();

            return task;
        }
    }
}