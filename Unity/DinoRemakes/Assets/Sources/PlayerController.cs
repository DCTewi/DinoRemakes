using UnityEngine;
using UnityEngine.InputSystem;

namespace DinoRemakes
{
    public sealed class PlayerController : MonoBehaviour
    {
        private static readonly string _JumpTriggerName = "Jump";
        private static readonly string _LandTriggerName = "Land";
        private static readonly string _GroundTag = "Ground";
        private static readonly string _ObstacleTag = "Obstacle";

        private Animator _animator;
        private Rigidbody2D _rigid;
        private AudioSource _jumpAudio;

        private readonly Vector2 _jumpForce = new(0, 900f);
        private bool _isOnGround = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _rigid = GetComponent<Rigidbody2D>();
            
            _jumpAudio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            GameManager.Instance.Input.Player.Jump.performed += OnJumpPerformed;
        }

        private void OnDestroy()
        {
            GameManager.Instance.Input.Player.Jump.performed -= OnJumpPerformed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (var contract in collision.contacts)
            {
                if (contract.collider.CompareTag(_GroundTag))
                {
                    _animator.SetTrigger(_LandTriggerName);
                    _isOnGround = true;
                    break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_ObstacleTag))
            {
                Debug.Log("Game Over!");
                GameManager.Instance.SetGameOver();
            }
        }

        private void OnJumpPerformed(InputAction.CallbackContext cb)
        {
            if (_isOnGround)
            {
                _jumpAudio.Play();
                _animator.SetTrigger(_JumpTriggerName);
                _rigid.AddForce(_jumpForce);
                _isOnGround = false;
            }
        }
    }
}
