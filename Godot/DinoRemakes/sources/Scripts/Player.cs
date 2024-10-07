using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts;

public partial class Player : RigidBody2D
{
    private static readonly StringName _PlayerJumpActionName = "player_jump";
    private static readonly StringName _DriveAnimationKey = "drive";
    private static readonly StringName _JumpAnimationKey = "jump";

    private bool _dirty = false;
    private Vector2 _originPosition;

    private readonly Vector2 _jumpForce = new(0, -115000);
    private bool _isOnFloor = false;

    private AnimatedSprite2D _sprite;
    private AudioStreamPlayer _audio;

    public override void _Ready()
    {
        base._Ready();

        _originPosition = Position;
        _sprite = GetNode<AnimatedSprite2D>("./Sprite");
        _audio = GetNode<AudioStreamPlayer>("./AudioStreamPlayer");

        BodyEntered += OnBodyEntered;

        Global.Instance.GameRestarted += OnGameRestarted;

    }

    private void OnGameRestarted()
    {
        if (_sprite is not null)
        {
            _sprite.Animation = _DriveAnimationKey;
        }
        Position = _originPosition;
        _dirty = true;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (_isOnFloor && @event.IsActionPressed(_PlayerJumpActionName))
        {
            _isOnFloor = false;
            _sprite.Animation = _JumpAnimationKey;
            _audio.Play();
            ApplyForce(_jumpForce);
        }
    }

    private void OnBodyEntered(Node body)
    {
        if (_sprite is not null)
        {
            _sprite.Animation = _DriveAnimationKey;
        }
        _isOnFloor = true;
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        base._IntegrateForces(state);
        if (_dirty)
        {
            state.LinearVelocity = Vector2.Zero;
            state.AngularVelocity = 0;
            state.Transform = state.Transform with { Origin = _originPosition };

            _dirty = false;
        }
    }
}
