using DinoRemakes.Core.Extensions;
using DinoRemakes.Core.Models;

using Stride.Engine;

using System;

namespace DinoRemakes.Core.Components
{
    public class GameManager : SyncScript
    {
        public override void Update()
        {
            Globals.State.TimeSinceBegin += Game.DeltaTimeAccurate();
            if (Globals.State.GameSpeed < GameState.MaxGameSpeed)
            {
                Globals.State.GameSpeed = (float)Math.Pow(GameState.MaxGameSpeed,
                    Globals.State.TimeSinceBegin / GameState.MaxGameSpeedDuration);
            }

            DebugText.Print($"Score: {Globals.State.Score}, GameSpeed: {Globals.State.GameSpeed}",
                new Stride.Core.Mathematics.Int2(10, 10));
        }
    }
}
