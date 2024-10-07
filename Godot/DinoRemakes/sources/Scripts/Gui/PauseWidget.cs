using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts.Gui;

public sealed partial class PauseWidget : CanvasLayer
{
    private Button _pauseButton;

    public override void _EnterTree()
    {
        base._EnterTree();

        ProcessMode = ProcessModeEnum.WhenPaused;
    }

    public override void _Ready()
    {
        base._Ready();

        _pauseButton = GetNode<Button>("%PauseButton");
        _pauseButton.Pressed += OnPauseButtonPressed;

        Global.Instance.GamePaused += SetActive;

        SetActive(false);
    }

    public void SetActive(bool active)
    {
        Visible = active;
    }

    private void OnPauseButtonPressed()
    {
        Global.Instance.SetPause(false);
        SetActive(false);
    }
}
