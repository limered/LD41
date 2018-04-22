namespace Systems.Interaction
{
    public class MessageDespawnTask
    {
        public MessageDespawnTask(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}