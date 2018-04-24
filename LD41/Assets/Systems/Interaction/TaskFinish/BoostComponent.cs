using SystemBase;
using UnityEngine;

namespace Systems.Interaction.TaskFinish
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BoostComponent : GameComponent
    {
        public float BoosterStrength;

        public string Name;
    }
}