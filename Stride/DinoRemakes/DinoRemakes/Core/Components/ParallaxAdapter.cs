using DinoRemakes.Core.Extensions;
using DinoRemakes.Core.Models;

using Stride.Core.Extensions;
using Stride.Engine;

using System;

namespace DinoRemakes.Core.Components
{
    public class ParallaxAdapter : SyncScript
    {
        public float MoveSpeed { get; set; } = -1f;
        public float MoveLength { get; set; } = 20f;

        protected float _jumpLength = 0f;
        public override void Start()
        {
            base.Start();

            MoveLength = Math.Abs(MoveLength);

            _jumpLength = 2 * MoveLength;
        }

        public override void Update()
        {
            var delta = Game.DeltaTime();

            Entity.GetChildren()
                .ForEach(entity =>
            {
                var position = entity.Transform.Position;

                position.X += MoveSpeed * delta * Globals.State.GameSpeed;

                if (MoveSpeed > 0f)
                {
                    if (position.X > MoveLength)
                    {
                        position.X -= _jumpLength;
                    }
                }
                else if (MoveSpeed < 0f)
                {
                    if (position.X < -MoveLength)
                    {
                        position.X += _jumpLength;
                    }
                }

                entity.Transform.Position = position;
            });
        }
    }
}
