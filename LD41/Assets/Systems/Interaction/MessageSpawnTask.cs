using Systems.GameState;

namespace Systems.Interaction
{
    public class MessageSpawnTask
    {
        public MessageSpawnTask(string name, Task task)
        {
            Name = name;
            Task = task;
        }
        public string Name { get; private set; }
        public Task Task { get; private set; }
    }
}