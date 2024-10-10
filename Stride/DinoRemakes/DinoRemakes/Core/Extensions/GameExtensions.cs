using Stride.Games;

namespace DinoRemakes.Core.Extensions
{
    public static class GameExtensions
    {
        public static float DeltaTime(this IGame game)
        {
            return (float)game.UpdateTime.Elapsed.TotalSeconds;
        }
        public static double DeltaTimeAccurate(this IGame game)
        {
            return game.UpdateTime.Elapsed.TotalSeconds;
        }
    }
}
