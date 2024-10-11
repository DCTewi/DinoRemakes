using Stride.Engine.Events;

namespace DinoRemakes.Core.Models
{
    public static class Globals
    {
        public static GameState State { get; set; } = new();

        public static readonly EventKey<bool> GamePausedEventKey = new("Game", "Paused");
        public static readonly EventKey GameOverEventKey = new("Game", "GameOver");
        public static readonly EventKey GameRestartEventKey = new("Game", "Restarted");

        public static readonly string GamePauseInputName = "Game_Pause";
        public static readonly string PlayerJumpInputName = "Player_Jump";

        public static readonly string GroundTag = "Ground";
        public static readonly string ObstacleTag = "Obstacle";
    }
}
