using UnityEngine;

namespace DinoRemakes
{
    public sealed class ObstacleParallaxer : Parallaxer
    {
        protected override void FixedUpdate()
        {
            var position = transform.position;

            position.x += MoveSpeed * (float)_gamestate.GameSpeed * Time.fixedDeltaTime;

            if (IsMovingToRight ? position.x > JumpX : position.x < JumpX)
            {
                Destroy(gameObject);
            }

            transform.position = position;
        }
    }
}
