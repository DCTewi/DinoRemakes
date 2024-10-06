using Godot;

namespace DinoRemakes.Sources.Models;

public sealed partial class GameState : Resource
{
    public static float MaxGameSpeed => 5.0f;
    public static float MaxGameSpeedDuration => 60.0f;

    [Export]
    public int Score { get; set; } = 0;

    [Export]
    public float GameSpeed { get; set; } = 1.0f;

    [Export]
    public double TimeSinceBegin { get; set; } = 0.0;

    public void Reset()
    {
        TimeSinceBegin = 0;
        Score = 0;
        GameSpeed = 1.0f;
    }
}
