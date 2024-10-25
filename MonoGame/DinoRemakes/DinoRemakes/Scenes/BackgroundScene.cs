using DinoRemakes.GameComponents;
using DinoRemakes.SourceGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace DinoRemakes.Scenes
{
    public class BackgroundScene : IScene
    {
        public List<IGameComponent> Components { get; private set; } = new()
        {
            new SpriteRenderer(
                ContentPath.Sprites.Sky_1,
                Color.White,
                new Rectangle(
                    0, 0,
                    DinoRemakesGame.Instance.Graphics.PreferredBackBufferWidth,
                    DinoRemakesGame.Instance.Graphics.PreferredBackBufferHeight),
                DinoRemakesGame.Instance.SpriteBatch),
        };
    }
}
