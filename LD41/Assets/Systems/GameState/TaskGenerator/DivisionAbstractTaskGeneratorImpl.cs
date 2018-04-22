using UnityEngine;

namespace Systems.GameState.TaskGenerator
{
    public class DivisionAbstractTaskGeneratorImpl : AbstractTaskGenerator
    {
        private readonly int _min;
        private readonly int _max;

        public DivisionAbstractTaskGeneratorImpl(int min, int max)
        {
            _min = min;
            _max = max;
        }
        public override Task Generate()
        {
            var task = new Task
            {
                Result = Random.Range(_min, _max),
                SecondNumber = Random.Range(_min, _max)
            };
            task.FirstNumber = task.Result * task.SecondNumber;
            task.Operation = "/";

            GenerateWrongs(task);

            task.PositionOfResult = GetResultPosition();

            return task;
        }
    }
}