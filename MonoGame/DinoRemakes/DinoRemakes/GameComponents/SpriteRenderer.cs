using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace DinoRemakes.GameComponents
{
    public class SpriteRenderer : IGameComponent, IDrawable, ILoadable
    {
        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public bool Visible { get; set; } = true;
        public int DrawOrder { get; set; } = 0;

        private Texture2D _texture;
        private readonly Color _color;
        private readonly Rectangle _destRectange;
        private readonly SpriteBatch _spriteBatch;
        private readonly string _path;

        public SpriteRenderer(string path, Color color, Rectangle destRectange, SpriteBatch spriteBatch)
        {
            _path = path;
            _spriteBatch = spriteBatch;
            _destRectange = destRectange;
            _color = color;
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>(_path);
        }

        public void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                _spriteBatch.Begin();

                _spriteBatch.Draw(_texture, _destRectange, _color);

                _spriteBatch.End();
            }
        }
    }
}
