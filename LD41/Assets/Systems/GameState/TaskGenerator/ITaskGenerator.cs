using System;

namespace Systems.GameState.TaskGenerator
{
    public interface ITaskGenerator
    {
        Task Generate(int min, int max);
    }
}
