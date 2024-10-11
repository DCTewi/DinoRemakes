using DinoRemakes.Core.Models;

using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;

namespace DinoRemakes.Core.Components
{
    public class PlayerController : SyncScript
    {
        private readonly EventReceiver<bool> _gamePauseListener = new(Globals.GamePausedEventKey);

        private CharacterComponent _charactor;

        public override void Start()
        {
            base.Start();

            _charactor = Entity.Get<CharacterComponent>();
        }

        public override void Update()
        {
            // TODO: input system
            if (Input.IsMouseButtonPressed(Stride.Input.MouseButton.Left))
            {
                _charactor.Jump();
            }
        }
    }
}
