using SystemBase;
using Systems.GameState.States.Messages;
using UniRx;

namespace Systems.UI
{
    [GameSystem]
    public class SplashscreenSystem : GameSystem<ScreenControlComponent, StartButtonComponent>
    {
        private SplashScreenComponent _splashScreen;
        public override void Init()
        {
            base.Init();
            MessageBroker.Default.Receive<MessageShowSplashScreen>()
                .Subscribe(OnShowSplashScreen);
        }

        public override void Register(ScreenControlComponent component)
        {
            _splashScreen = component.SplashScreen;
        }

        private void OnShowSplashScreen(MessageShowSplashScreen messageShowSplashScreen)
        {
            if(_splashScreen)
            { _splashScreen.gameObject.SetActive(true);}
        }

        public override void Register(StartButtonComponent component)
        {
            component.OnButtonClick.Subscribe(_ =>
            {
                _splashScreen.gameObject.SetActive(false);
                MessageBroker.Default.Publish(new MessageStartGame());
            });
        }
    }
}