using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts;

public sealed partial class Obstacle : AnimatableBody2D
{
    [Export]
    public Vector2 MoveSpeed { get; set; }

    private VisibleOnScreenNotifier2D _notifier;
    private bool _enteredScreen = false;

    public override void _Ready()
    {
        base._Ready();

        _notifier = GetNode<VisibleOnScreenNotifier2D>("./Notifier");
        _notifier.ScreenEntered += () => _enteredScreen = true;
        _notifier.ScreenExited += OnScreenExited;
    }

    private void OnScreenExited()
    {
        if (_enteredScreen)
        {
            QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        var motion = MoveSpeed * Global.GameState.GameSpeed * (float)delta;

        var collision = MoveAndCollide(motion);
        if (collision?.GetCollider() is Player)
        {
            Global.Instance.SetGameOver();
        }
    }
}
