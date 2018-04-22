using SystemBase;
using UnityEngine;

namespace Systems.Interaction.Obstacles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ObstacleComponent : GameComponent
    {
        public string ParentName { get; set; }
    }
}