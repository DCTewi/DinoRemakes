using DinoRemakes.Core.Models;

using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;

using System.Threading.Tasks;

namespace DinoRemakes.Core.Components
{
    public class ObstacleParallaxAdapter : SyncScript
    {
        public Vector3 MoveSpeed { get; set; } = new(-8, 0, 0);
        public float DestoryX { get; set; } = -20f;

        private readonly EventReceiver _gameRestartListener = new(Globals.GameRestartEventKey);

        private RigidbodyComponent _rigid;
        public override void Start()
        {
            base.Start();

            _rigid = Entity.Get<RigidbodyComponent>();

            Script.AddTask(ListenToRestartEvent);
        }

        public override void Update()
        {
            if (Globals.State.Paused)
            {
                return;
            }

            _rigid.LinearVelocity = MoveSpeed * Globals.State.GameSpeed;

            if (Entity.Transform.Position.X < DestoryX)
            {
                Entity.GetParent().RemoveChild(Entity);
            }
        }

        private async Task ListenToRestartEvent()
        {
            while (Game.IsRunning && Entity != null)
            {
                await _gameRestartListener.ReceiveAsync();
                Entity.GetParent()?.RemoveChild(Entity);
            }
        }
    }
}
