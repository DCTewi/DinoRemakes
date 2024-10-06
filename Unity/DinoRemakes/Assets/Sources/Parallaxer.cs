using UnityEngine;

namespace DinoRemakes
{
    public class Parallaxer : MonoBehaviour
    {
        [field: SerializeField]
        public float JumpX { get; set; } = -20f;

        [field: SerializeField]
        public float DestX { get; set; } = 20f;

        [field: SerializeField]
        public float MoveSpeed { get; set; } = -1.0f;

        public bool IsMovingToRight => MoveSpeed > 0f;

        protected GameState _gamestate;

        protected void Start()
        {
            _gamestate = GameManager.Instance.GameState;
        }

        protected virtual void FixedUpdate()
        {
            var position = transform.position;

            position.x += MoveSpeed * (float)_gamestate.GameSpeed * Time.fixedDeltaTime;

            if (IsMovingToRight ? position.x > JumpX : position.x < JumpX)
            {
                position.x = DestX;
            }

            transform.position = position;
        }
    }
}
