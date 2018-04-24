using System;
using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Utils;

namespace Systems.GameState.Time
{
    [GameSystem]
    public class TimerSystem : GameSystem<TimerUiComponent>
    {
        private IDisposable _updateDisposable;
        

        public override void Register(TimerUiComponent component)
        {
            MessageBroker.Default.Receive<MessageTimerStart>()
                .Select(_=>component)
                .Subscribe(OnTimerStart)
                .AddTo(IoC.Game);
            MessageBroker.Default.Receive<MessageTimerStop>()
                .Select(_ => component)
                .Subscribe(OnTimerStop)
                .AddTo(IoC.Game);
        }

        private void OnTimerStop(TimerUiComponent component)
        {
            _updateDisposable.Dispose();
        }

        private void OnTimerStart(TimerUiComponent component)
        {
            component.Time = 0;
            PrintTime(component);
            _updateDisposable = IoC.Game.UpdateAsObservable()
                .Select(_ => component)
                .Subscribe(OnGameUpdate);
        }

        private void OnGameUpdate(TimerUiComponent component)
        {
            component.Time += UnityEngine.Time.deltaTime;

            PrintTime(component);
        }

        private static void PrintTime(TimerUiComponent component)
        {
            var minutes = (int)component.Time / 60;
            var seconds = (int)component.Time % 60;
            var millies = ((component.Time - seconds) * 100) % 100;

            var text = component.GetComponent<Text>();
            text.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millies);
        }
    }
}
