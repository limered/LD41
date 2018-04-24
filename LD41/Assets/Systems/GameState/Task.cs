using System;
using Systems.GameState.States;

namespace Systems.GameState
{
    [Serializable]
    public class Task
    {
        public int FirstNumber;
        public int SecondNumber;
        public string Operation;
        public int Result;
        public int Wrong1;
        public int Wrong2;
        public TrackPosition PositionOfResult;
    }
}