namespace Systems.Interaction.Obstacles
{
    public class MessageObstacleDespawn
    {
        public MessageObstacleDespawn(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}