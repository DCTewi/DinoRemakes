using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace DinoRemakes.GameComponents
{
    public class SpriteRenderer(
        string path, Color color, Rectangle destRectange, SpriteBatch spriteBatch)
        : IGameComponent, IDrawable, ILoadable
    {
        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public bool Visible { get; set; } = true;
        public int DrawOrder { get; set; } = 0;

        private Texture2D _texture;

        public void Initialize() { }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>(path);
            Console.WriteLine(_texture);
        }

        public void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                spriteBatch.Begin();

                Console.WriteLine(_texture);
                spriteBatch.Draw(_texture, destRectange, color);

                spriteBatch.End();
            }
        }
    }
}
