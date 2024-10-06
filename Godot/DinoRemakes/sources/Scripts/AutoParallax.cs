using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts;

public partial class AutoParallax : Parallax2D
{
    private Vector2 _originSpeed;

    public override void _EnterTree()
    {
        base._EnterTree();

        _originSpeed = Autoscroll;
    }

    public override void _Process(double delta)
    {
        base._PhysicsProcess(delta);

        Autoscroll = _originSpeed * Global.GameState.GameSpeed;
    }
}
