using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace DinoRemakes.Scenes
{
    public interface IScene
    {
        List<IGameComponent> Components { get; }
    }
}
