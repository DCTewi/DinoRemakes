using Godot;
using System;

namespace DinoRemakes.Sources.Scripts;

public partial class Player : RigidBody2D
{
    private static readonly StringName _PlayerJumpActionName = "player_jump";
    private static readonly StringName _DriveAnimationKey = "drive";
    private static readonly StringName _JumpAnimationKey = "jump";

    private readonly Vector2 _jumpForce = new(0, -115000);
    private bool _isOnFloor = false;

    private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
        base._Ready();

        _sprite = GetNode<AnimatedSprite2D>("./Sprite");

        BodyEntered += OnBodyEntered;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (_isOnFloor && @event.IsActionPressed(_PlayerJumpActionName))
        {
            _isOnFloor = false;
            _sprite.Animation = _JumpAnimationKey;
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
}
