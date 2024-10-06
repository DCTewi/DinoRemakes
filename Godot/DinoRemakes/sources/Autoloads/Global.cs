using DinoRemakes.Sources.Models;

using Godot;

using System;

namespace DinoRemakes.Sources.Autoloads;

public sealed partial class Global : Node
{
    public static Global Instance { get; private set; }

    public static GameState GameState => Instance._state;
    [Export] private GameState _state = new();

    public override void _EnterTree()
    {
        base._EnterTree();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            QueueFree();
            return;
        }

        ProcessMode = ProcessModeEnum.Always;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        _state.TimeSinceBegin += delta;
        _state.Score = (int)(_state.TimeSinceBegin * 10.0);
        if (_state.GameSpeed < GameState.MaxGameSpeed)
        {
            _state.GameSpeed = (float)Math.Pow(GameState.MaxGameSpeed,
                _state.TimeSinceBegin / GameState.MaxGameSpeedDuration);
        }
    }
}
