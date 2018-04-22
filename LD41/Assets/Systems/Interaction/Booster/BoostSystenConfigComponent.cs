using SystemBase;
using UnityEngine;

namespace Systems.Interaction.Booster
{
    public class BoostSystenConfigComponent : GameComponent
    {
        public float MinStrength;
        public float MaxStrength;
        public float LoadPerUnit;
        public GameObject BoosterPrefab;
    }
}