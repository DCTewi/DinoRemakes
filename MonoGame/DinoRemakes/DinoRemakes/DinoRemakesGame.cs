using DinoRemakes.GameComponents;
using DinoRemakes.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace DinoRemakes
{
    public class DinoRemakesGame : Game
    {
        public static DinoRemakesGame Instance { get; private set; }

        public GraphicsDeviceManager Graphics { get; }
        public SpriteBatch SpriteBatch => _spriteBatch;

        private readonly List<IScene> _scenes = [];
        private SpriteBatch _spriteBatch;

        public DinoRemakesGame()
        {
            Instance = Instance == null ? this : throw new InvalidProgramException();

            Graphics = new(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new(GraphicsDevice);

            _scenes.Add(new BackgroundScene());


            foreach (var scene in _scenes)
            {
                foreach (var component in scene.Components)
                {
                    Components.Add(component);
                }
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (var scene in _scenes)
            {
                foreach (var component in scene.Components)
                {
                    if (component is ILoadable loadable)
                    {
                        loadable.LoadContent(Content);
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
