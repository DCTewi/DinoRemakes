using DinoRemakes.Core.Extensions;
using DinoRemakes.Core.Models;

using Stride.Core.Extensions;
using Stride.Core.Mathematics;
using Stride.Engine;

using System;

namespace DinoRemakes.Core.Components
{
    public class RandomParallaxAdapter : SyncScript
    {
        public Vector2 MoveSpeedRange { get; set; } = new(-0.5f, -0.9f);
        public Vector2 ScaleRange { get; set; } = new(0.8f, 1.2f);
        public float MoveLength { get; set; } = 20f;

        private float _jumpLength = 0f;
        private readonly Random _random = new();

        public override void Start()
        {
            base.Start();

            MoveLength = Math.Abs(MoveLength);

            _jumpLength = 2 * MoveLength;

            var scaleLength = Math.Abs(ScaleRange.Y - ScaleRange.X);
            Entity.GetChildren()
                .ForEach(entity =>
                {
                    var scale = _random.NextSingle() * scaleLength + ScaleRange.X;
                    entity.Transform.Scale = Vector3.One * scale;
                });
        }

        public override void Update()
        {
            var delta = Game.DeltaTime();

            Entity.GetChildren()
                .ForEach(entity =>
                {
                    var position = entity.Transform.Position;
                    var speed = GetSpeed(entity);

                    position.X += speed * delta * Globals.State.GameSpeed;

                    if (speed > 0f)
                    {
                        if (position.X > MoveLength)
                        {
                            position.X -= _jumpLength;
                        }
                    }
                    else if (speed < 0f)
                    {
                        if (position.X < -MoveLength)
                        {
                            position.X += _jumpLength;
                        }
                    }

                    entity.Transform.Position = position;
                });
        }

        private float GetSpeed(Entity entity)
        {
            var scale = entity.Transform.Scale.X;
            var factor = (scale - ScaleRange.X) / (ScaleRange.Y - ScaleRange.X);

            return MoveSpeedRange.X + factor * (MoveSpeedRange.Y - MoveSpeedRange.X);
        }
    }
}
