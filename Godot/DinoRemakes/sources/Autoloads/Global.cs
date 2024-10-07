using DinoRemakes.Core.Save;
using DinoRemakes.Sources.Models;

using Godot;

using System;

namespace DinoRemakes.Sources.Autoloads;

public sealed partial class Global : Node
{
    private static readonly string _GamePauseActionName = "game_pause";

    public static Global Instance { get; private set; }

    public static SaveData Save
    {
        get => _SaveData.Load();
        set => _SaveData = value.Save();
    }
    private static SaveData _SaveData = new();

    public static GameState GameState => Instance._state;
    [Export] private GameState _state = new();

    public event Action<bool> GamePaused;
    public event Action GameRestarted;
    public event Action GameOver;


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

        if (!GetTree().Paused)
        {
            _state.TimeSinceBegin += delta;
            _state.Score = (int)(_state.TimeSinceBegin * 10.0);
            if (_state.GameSpeed < GameState.MaxGameSpeed)
            {
                _state.GameSpeed = (float)Math.Pow(GameState.MaxGameSpeed,
                    _state.TimeSinceBegin / GameState.MaxGameSpeedDuration);
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed(_GamePauseActionName))
        {
            SetPause(!GetTree().Paused);
        }
    }

    public void SetPause(bool paused)
    {
        GetTree().Paused = paused;
        GamePaused?.Invoke(paused);
    }

    public void SetGameOver()
    {
        GetTree().Paused = true;
        GameOver?.Invoke();
    }

    public void RestartGame()
    {
        _state.Reset();
        GetTree().Paused = false;
        GameRestarted?.Invoke();
    }
}
