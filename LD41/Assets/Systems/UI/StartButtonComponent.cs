using SystemBase;
using UniRx;

namespace Systems.UI
{
    public class StartButtonComponent : GameComponent
    {
        public ReactiveCommand OnButtonClick = new ReactiveCommand();

        public void OnClick()
        {
            OnButtonClick.Execute();
        }
    }
}