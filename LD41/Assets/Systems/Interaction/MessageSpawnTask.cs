namespace Systems.Interaction
{
    public class MessageSpawnTask
    {
        public MessageSpawnTask(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}