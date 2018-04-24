using SystemBase;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.GameState.Time
{
    [RequireComponent(typeof(Text))]
    public class TimerUiComponent : GameComponent
    {
        public float Time;
    }
}