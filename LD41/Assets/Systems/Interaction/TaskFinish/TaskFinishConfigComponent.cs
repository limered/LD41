using SystemBase;
using UnityEngine;

namespace Systems.Interaction.TaskFinish
{
    public class TaskFinishConfigComponent : GameComponent
    {
        public float BoosterStrength;
        public GameObject BoostPrefab;
        public GameObject ObstaclePrefab;
        public float ObstacleStrength;
    }
}