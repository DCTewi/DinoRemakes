using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts.Gui;

public sealed partial class GameOverWidget : CanvasLayer
{
    private Label _scoreLabel;
    private Button _restartButton;

    public override void _EnterTree()
    {
        base._EnterTree();

        ProcessMode = ProcessModeEnum.WhenPaused;
    }


    public override void _Ready()
    {
        base._Ready();

        _scoreLabel = GetNode<Label>("%ScoreLabel");
        _restartButton = GetNode<Button>("%RestartButton");
        _restartButton.Pressed += OnRestartButtonPressed;

        Global.Instance.GameOver += OnGameOver;

        Visible = false;
    }

    private void OnRestartButtonPressed()
    {
        Visible = false;
        Global.Instance.RestartGame();
    }

    private void OnGameOver()
    {
        var save = Global.Save;

        var currentScore = Global.GameState.Score;
        var bestScore = save.BestScore;

        if (currentScore > bestScore)
        {
            save.BestScore = currentScore;
            Global.Save = save;
        }

        _scoreLabel.Text = $"本次得分: {currentScore}\n最高得分: {save.BestScore}";

        Visible = true;
    }
}
