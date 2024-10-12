using DinoRemakes.Core.Models;

using Stride.Audio;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;

using System.Threading.Tasks;

namespace DinoRemakes.Core.Components
{
    public class PlayerController : SyncScript
    {
        private readonly EventReceiver _gameRestartListener = new(Globals.GameRestartEventKey);

        // there's no sth like audio stream player? really?
        public Sound JumpAudio { get; set; }
        private SoundInstance _jumpAudio;

        private CharacterComponent _charactor;
        private SpriteAnimator _anim;

        private bool _isOnFloor = true;
        private Vector3 _originPosition;
        public override void Start()
        {
            base.Start();

            _jumpAudio = JumpAudio.CreateInstance();
            _jumpAudio.IsLooping = false;

            _charactor = Entity.Get<CharacterComponent>();
            _anim = Entity.Get<SpriteAnimator>();
            _anim.Play("drive");
            _originPosition = Entity.Transform.Position;

            Script.AddTask(WaitLandOnFloor);
            Script.AddTask(ListenRestartEvent);
        }

        public override void Update()
        {
            if (_isOnFloor && Input.IsVirtualButtonPressed(0, Globals.PlayerJumpInputName))
            {
                _jumpAudio.Play();
                _anim.Play("jump");
                _charactor.Jump();
                _isOnFloor = false;
            }
        }

        public async Task WaitLandOnFloor()
        {
            while (Game.IsRunning)
            {
                var collision = await _charactor.NewCollision();
                foreach (var contract in collision.Contacts)
                {
                    if (contract.ColliderB?.Tag == Globals.GroundTag)
                    {
                        _isOnFloor = true;
                        _anim.Play("drive");
                        break;
                    }
                    if (contract.ColliderB?.Tag == Globals.ObstacleTag)
                    {
                        Globals.GameOverEventKey.Broadcast();
                        break;
                    }
                }
            }
        }

        public async Task ListenRestartEvent()
        {
            while (Game.IsRunning)
            {
                await _gameRestartListener.ReceiveAsync();

                Entity.Transform.Position = _originPosition;
                _charactor.Simulation.ClearForces();
                _charactor.SetVelocity(Vector3.Zero);
                _charactor.Teleport(_originPosition);
                _charactor.UpdatePhysicsTransformation();
            }
        }
    }
}
