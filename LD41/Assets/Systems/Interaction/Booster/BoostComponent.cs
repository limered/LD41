using SystemBase;
using UnityEngine;

namespace Systems.Interaction.Booster
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BoostComponent : GameComponent
    {
        public float BoosterStrength;

        public string Name;
    }
}