using System;
using SystemBase;
using Systems.GameState.Time;
using GameSparks.Api.Responses;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems.GameState.Scoreboard
{
    [GameSystem(typeof(TimerSystem))]
    public class ScoreBoardSystem : GameSystem<ScoreBoardComponent, TimerUiComponent, PlayerNameComponent>
    {
        private PlayerNameComponent _playerName;
        private string _randomName;
        private ScoreBoardComponent _scoreBoard;

        public override void Init()
        {
            base.Init();
            _randomName = string.Format("NoName{0}", Random.Range(10000, 99999));
        }

        public override void Register(ScoreBoardComponent component)
        {
            _scoreBoard = component;
            LoadScores();
        }

        private void LoadScores()
        {
            var childs = _scoreBoard.transform.childCount;
            for (var i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(_scoreBoard.transform.GetChild(i).gameObject);
            }

            new GameSparks.Api.Requests.LeaderboardDataRequest()
                .SetEntryCount(10)
                .SetLeaderboardShortCode("SCORES")
                .Send(OnLeaderBoardLoaded);
        }

        private void OnLeaderBoardLoaded(LeaderboardDataResponse leaderboardDataResponse)
        {
            foreach (var leaderboardData in leaderboardDataResponse.Data)
            {
                var line = Object.Instantiate(_scoreBoard.ScoreLinePrefab, _scoreBoard.transform);
                var lineComponent = line.GetComponent<ScoreLineComponent>();
                lineComponent.PlayerName.text = leaderboardData.UserName;
                var score = leaderboardData.GetNumberValue("SCORE");
                if (score.HasValue)
                {
                    var minutes = score / 60000;
                    var seconds = (score / 1000) % 60;
                    var millies = score % 1000;

                    lineComponent.PlayerTime.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millies);
                }
            }
        }

        public override void Register(TimerUiComponent component)
        {
            MessageBroker.Default.Receive<MessageTimerStop>()
                .Select(m => component)
                .Subscribe(OnTimerStopped);

            MessageBroker.Default.Receive<MessageTimerStart>()
                .Subscribe(ChangeNameOnApi);
        }

        private void ChangeNameOnApi(MessageTimerStart messageTimerStart)
        {
            var userName = GetPlayerName();

            new GameSparks.Api.Requests.AuthenticationRequest()
                .SetUserName(userName)
                .SetPassword("123456")
                .Send(loginResponse =>{
                    if (loginResponse.HasErrors)
                    {
                        new GameSparks.Api.Requests.RegistrationRequest()
                            .SetUserName(userName)
                            .SetDisplayName(userName)
                            .SetPassword("123456")
                            .Send(r =>
                            {
                                Debug.Log(!r.HasErrors);
                                new GameSparks.Api.Requests.AuthenticationRequest()
                                    .SetUserName(userName)
                                    .SetPassword("123456")
                                    .Send(response => Debug.Log(!response.HasErrors));
                            });
                    }
                });
        }

        private void OnTimerStopped(TimerUiComponent timerUiComponent)
        {
            var time = timerUiComponent.Time * 1000;
            var name = GetPlayerName();

            new GameSparks.Api.Requests.LogEventRequest()
                .SetEventKey("SUBMIT_SCORE")
                .SetEventAttribute("SCORE", time.ToString())
                .SetEventAttribute("PLAYER_NAME", name)
                .Send(OnResponse);
        }

        private string GetPlayerName()
        {
            var nameText = _playerName.GetComponent<Text>();
            return string.IsNullOrEmpty(nameText.text) ? _randomName : nameText.text;
        }

        private void OnResponse(LogEventResponse logEventResponse)
        {
            if (!logEventResponse.HasErrors)
            {
                Debug.Log("Score Posted Successfully...");
            }
            else
            {
                Debug.Log("Error Posting Score...");
            }
            LoadScores();
        }

        public override void Register(PlayerNameComponent component)
        {
            _playerName = component;
        }
    }
}
