using SystemBase;
using UnityEngine;

namespace Systems.Interaction.TaskFinish
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ObstacleComponent : GameComponent
    {
        public string Name { get; set; }
    }
}