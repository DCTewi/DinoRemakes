using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts.Gui;

public sealed partial class ScoreWidget : CanvasLayer
{
    private static readonly int _SilverScore = 100;
    private static readonly int _GoldScore = 600;

    [Export]
    public CompressedTexture2D SilverMedal { get; set; }

    [Export]
    public CompressedTexture2D GoldMedal { get; set; }

    private TextureRect _medalImage;
    private Label _scoreLabel;

    private int _lastScore = 0;

    public override void _Ready()
    {
        base._Ready();

        _medalImage = GetNode<TextureRect>("%MedalImage");
        _scoreLabel = GetNode<Label>("%ScoreLabel");

        Visible = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_lastScore < _SilverScore)
        {
            _lastScore = Global.GameState.Score;
            if (_lastScore >= _SilverScore)
            {
                _medalImage.Texture = SilverMedal;
            }
        }
        else if (_lastScore < _GoldScore)
        {
            _lastScore = Global.GameState.Score;
            if (_lastScore >= _GoldScore)
            {
                _medalImage.Texture = GoldMedal;
            }
        }

        _scoreLabel.Text = $"得分: {Global.GameState.Score}";
    }
}
