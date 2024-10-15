using DinoRemakes.GameComponents;
using DinoRemakes.SourceGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace DinoRemakes
{
    public class DinoRemakesGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly List<IGameComponent> _components = [];

        public DinoRemakesGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _components.Add(new SpriteRenderer(
                ContentPath.Sprites.Sky_1,
                Color.White,
                new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
                _spriteBatch));

            _components.Add(new SpriteRenderer(
                ContentPath.Sprites.Tile_dirt_1,
                Color.White,
                new Rectangle(100, 100, 70, 70),
                _spriteBatch));

            foreach (var component in _components)
            {
                Components.Add(component);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (var component in _components)
            {
                if (component is ILoadable loadable)
                {
                    loadable.LoadContent(Content);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
