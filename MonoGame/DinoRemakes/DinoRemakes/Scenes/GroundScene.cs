using DinoRemakes.GameComponents;
using DinoRemakes.SourceGenerator;

using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace DinoRemakes.Scenes
{
    public class GroundScene : IScene
    {
        public List<IGameComponent> Components { get; init; } =
        [
            // yes it's ugly just because i'm too lazy to repeat sth 
            // like ParallaxedTilemapSprite again

            // layer 1
            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_3,
                new Vector2(0, 855),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_3,
                new Vector2(140 * 5, 855),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_3,
                new Vector2(140 * 10, 855),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_3,
                new Vector2(140 * 15, 855),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            // layer 2
            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_6,
                new Vector2(0, 995),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_6,
                new Vector2(140 * 5, 995),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_6,
                new Vector2(140 * 10, 995),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),

            new ParallaxedSprite(
                ContentPath.Sprites.Tile_dirt_6,
                new Vector2(140 * 15, 995),
                DinoRemakesGame.Instance.SpriteBatch,
                640,
                2.0f,
                repeatHalfCount: 10),
        ];
    }
}
