using System;

namespace DinoRemakes.Core.Models
{
    public class GameState
    {
        public static double MaxGameSpeed => 5.0;
        public static double MaxGameSpeedDuration => 60.0;
        public float GameSpeed { get; set; } = 1.0f;

        public int Score => (int)Math.Floor(TimeSinceBegin * 10.0);
        public double TimeSinceBegin { get; set; } = 0.0;

        public bool Paused { get; set; } = false;
        public bool GameOver { get; set; } = false;

        public void Reset()
        {
            GameSpeed = 1.0f;
            TimeSinceBegin = 0.0;
            Paused = false;
            GameOver = false;
        }
    }
}
