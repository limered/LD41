using SystemBase;
using Systems.GameState.States;

namespace Systems.Interaction.TaskVisualization
{
    public class StreetWriterComponent : GameComponent
    {
        public string Name;
        public StreetWriterType Type;
        public TrackPosition StreetPos;
        public string Value;
    }
}