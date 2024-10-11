using DinoRemakes.Core.Extensions;
using DinoRemakes.Core.Models;

using Stride.Engine;
using Stride.Engine.Events;

using System;
using System.Threading.Tasks;

namespace DinoRemakes.Core.Components
{
    public class GameManager : SyncScript
    {
        private readonly EventReceiver<bool> _gamePauseListener = new(Globals.GamePausedEventKey);

        public override void Start()
        {
            base.Start();

            Script.AddTask(ListenPauseEvent);
        }

        public override void Update()
        {
            if (!Globals.State.Paused)
            {
                Globals.State.TimeSinceBegin += Game.DeltaTimeAccurate();
                if (Globals.State.GameSpeed < GameState.MaxGameSpeed)
                {
                    Globals.State.GameSpeed = (float)Math.Pow(GameState.MaxGameSpeed,
                        Globals.State.TimeSinceBegin / GameState.MaxGameSpeedDuration);
                }
            }

            DebugText.Print($"Score: {Globals.State.Score}, GameSpeed: {Globals.State.GameSpeed}",
                new Stride.Core.Mathematics.Int2(10, 10));

            if (Input.IsKeyPressed(Stride.Input.Keys.Escape))
            {
                var pause = !Globals.State.Paused;
                Globals.State.Paused = pause;
                Globals.GamePausedEventKey.Broadcast(pause);
            }
        }

        public async Task ListenPauseEvent()
        {
            while (Game.IsRunning)
            {
                var paused = await _gamePauseListener.ReceiveAsync();

                Game.UpdateTime.Factor = paused ? 0 : 1;
            }

        }
    }
}
