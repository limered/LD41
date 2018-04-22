using System;
using SystemBase;
using Systems.GameState.States;
using UniRx;
using UnityEngine.UI;
using Utils;

namespace Systems.Interaction.TaskVisualization
{
    [GameSystem]
    public class StreetWriterSystem : GameSystem<StreetWriterComponent>
    {
        public override void Register(StreetWriterComponent component)
        {
            MessageBroker.Default.Receive<MessageSpawnTask>()
                .Select(m => new Tuple<MessageSpawnTask, StreetWriterComponent>(m, component))
                .Where(tuple => tuple.Item1.Name.Equals(tuple.Item2.Name))
                .Subscribe(OnShowValue)
                .AddTo(IoC.Game);
        }

        private void OnShowValue(Tuple<MessageSpawnTask, StreetWriterComponent> tuple)
        {
            var text = tuple.Item2.GetComponent<Text>();
            switch (tuple.Item2.Type)
            {
                case StreetWriterType.FirstNumber:
                    text.text = tuple.Item1.Task.FirstNumber.ToString();
                    break;
                case StreetWriterType.SecondNumber:
                    text.text = tuple.Item1.Task.SecondNumber.ToString();
                    break;
                case StreetWriterType.Operation:
                    text.text = tuple.Item1.Task.Operation;
                    break;
                case StreetWriterType.Result:
                    SetResultText(text, tuple);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetResultText(Text text, Tuple<MessageSpawnTask, StreetWriterComponent> tuple)
        {
            if (tuple.Item2.StreetPos == tuple.Item1.Task.PositionOfResult)
            {
                text.text = tuple.Item1.Task.Result.ToString();
            }
            else
            {
                switch (tuple.Item1.Task.PositionOfResult)
                {
                    case TrackPosition.Left:
                        text.text = (tuple.Item2.StreetPos == TrackPosition.Middle)
                            ? tuple.Item1.Task.Wrong1.ToString()
                            : tuple.Item1.Task.Wrong2.ToString();
                        break;
                    case TrackPosition.Middle:
                        text.text = (tuple.Item2.StreetPos == TrackPosition.Left)
                            ? tuple.Item1.Task.Wrong1.ToString()
                            : tuple.Item1.Task.Wrong2.ToString();
                        break;
                    case TrackPosition.Right:
                        text.text = (tuple.Item2.StreetPos == TrackPosition.Left)
                            ? tuple.Item1.Task.Wrong1.ToString()
                            : tuple.Item1.Task.Wrong2.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public enum StreetWriterType
    {
        FirstNumber,
        SecondNumber,
        Operation,
        Result
    }
}
