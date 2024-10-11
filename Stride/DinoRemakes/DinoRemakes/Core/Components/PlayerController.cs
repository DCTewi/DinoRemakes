using DinoRemakes.Core.Models;

using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;

using System.Threading.Tasks;

namespace DinoRemakes.Core.Components
{
    public class PlayerController : SyncScript
    {
        private readonly EventReceiver<bool> _gamePauseListener = new(Globals.GamePausedEventKey);

        private CharacterComponent _charactor;

        private bool _isOnFloor = false;

        public override void Start()
        {
            base.Start();

            _charactor = Entity.Get<CharacterComponent>();

            Script.AddTask(WaitLandOnFloor);
        }

        public override void Update()
        {
            if (_isOnFloor && Input.IsVirtualButtonPressed(0, Globals.PlayerJumpInputName))
            {
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
    }
}
