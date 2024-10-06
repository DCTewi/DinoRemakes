using System;
using UnityEngine;

namespace DinoRemakes
{
    // Oh no unity only use C# 9.0
    [Serializable]
    public sealed class GameState
    {
        [field: SerializeField]
        public double TimeSinceBegin { get; set; } = 0;

        [field: SerializeField]
        public int Score { get; set; } = 0;

        [field: SerializeField]
        public bool Paused { get; set; } = false;

        [field: SerializeField]
        public bool GameOver { get; set; } = false;

        [field: SerializeField]
        public double GameSpeed { get; set; } = 1.0;

        [field: SerializeField]
        public double MaxGameSpeed { get; } = 5.0;

        [field: SerializeField]
        public double MaxGameSpeedDuration { get; } = 60.0;

        public void Reset()
        {
            TimeSinceBegin = 0;
            Score = 0;
            Paused = false;
            GameOver = false;
            GameSpeed = 1.0;
        }
    }
}