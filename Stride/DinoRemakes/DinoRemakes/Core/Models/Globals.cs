using Stride.Engine.Events;

namespace DinoRemakes.Core.Models
{
    public static class Globals
    {
        public static GameState State { get; set; } = new();

        public static readonly EventKey<bool> GamePausedEventKey = new("Game", "Paused");
        public static readonly EventKey GameOverEventKey = new("Game", "GameOver");
        public static readonly EventKey GameRestartEventKey = new("Game", "Restarted");
    }
}
