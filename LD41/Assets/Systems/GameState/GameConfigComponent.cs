using SystemBase;
using Systems.GameState.TaskGenerator;

namespace Systems.GameState
{
    public class GameConfigComponent : GameComponent
    {
        public ITaskGenerator[] FirstRoundGenerators =
        {
            new AdditionAbstractTaskGenerator(),
            new SubtractionAbstractTaskGenerator()
        };

        public ITaskGenerator[] SecondRoundGenerators =
        {
            new AdditionAbstractTaskGenerator(),
            new SubtractionAbstractTaskGenerator(),
            new MultiplyAbstractTaskGeneratorImpl()
        };

        public ITaskGenerator[] ThirdRoundGenerators =
        {
            new AdditionAbstractTaskGenerator(),
            new SubtractionAbstractTaskGenerator(),
            new MultiplyAbstractTaskGeneratorImpl(),
            new DivisionAbstractTaskGeneratorImpl()
        };
    }
}