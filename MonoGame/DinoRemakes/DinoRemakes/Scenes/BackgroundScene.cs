using DinoRemakes.GameComponents;
using DinoRemakes.SourceGenerator;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

namespace DinoRemakes.Scenes
{
    public class BackgroundScene : IScene
    {
        public List<IGameComponent> Components { get; init; } =
        [
            // background sky
            new SpriteRenderer(
                ContentPath.Sprites.Sky_1,
                Color.White,
                new Rectangle(
                    0, 0,
                    DinoRemakesGame.Instance.Graphics.PreferredBackBufferWidth,
                    DinoRemakesGame.Instance.Graphics.PreferredBackBufferHeight),
                DinoRemakesGame.Instance.SpriteBatch),

            // background clouds
            new ParallaxedSprite(
                ContentPath.Sprites.Clouds1,
                new Vector2(0, 350),
                DinoRemakesGame.Instance.SpriteBatch,
                40,
                2.0f),

            // background hills
            new ParallaxedSprite(
                ContentPath.Sprites.Hills_1,
                new Vector2(0, 600),
                DinoRemakesGame.Instance.SpriteBatch,
                120,
                2.0f),
        ];

        private readonly Random _random = new();

        public BackgroundScene()
        {
            var path = ContentPath.Sprites.Cloud_1[..^1];

            // small clouds
            for (int i = 1; i <= 5; ++i)
            {
                var scale = (_random.NextSingle() * 0.5f) + 0.8f;

                Components.Add(new ParallaxedSprite(
                    path + i,
                    new Vector2(_random.Next(-500, 3840), _random.Next(-10, 220)),
                    DinoRemakesGame.Instance.SpriteBatch,
                    60 * scale,
                    scale: scale,
                    gap: 1920));
            }
        }
    }
}
