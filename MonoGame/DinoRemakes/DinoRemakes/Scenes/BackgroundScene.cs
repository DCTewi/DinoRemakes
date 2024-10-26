using DinoRemakes.GameComponents;
using DinoRemakes.SourceGenerator;

using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace DinoRemakes.Scenes
{
    public class BackgroundScene : IScene
    {
        public List<IGameComponent> Components { get; private set; } =
        [
            new SpriteRenderer(
                ContentPath.Sprites.Sky_1,
                Color.White,
                new Rectangle(
                    0, 0,
                    DinoRemakesGame.Instance.Graphics.PreferredBackBufferWidth,
                    DinoRemakesGame.Instance.Graphics.PreferredBackBufferHeight),
                DinoRemakesGame.Instance.SpriteBatch),

            new ParallaxedSprite(
                ContentPath.Sprites.Clouds1,
                new Vector2(0, 350),
                DinoRemakesGame.Instance.SpriteBatch,
                40,
                2.0f),

            new ParallaxedSprite(
                ContentPath.Sprites.Hills_1,
                new Vector2(0, 600),
                DinoRemakesGame.Instance.SpriteBatch,
                120,
                2.0f),
        ];
    }
}
