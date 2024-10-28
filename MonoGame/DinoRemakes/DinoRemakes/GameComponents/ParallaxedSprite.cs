using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace DinoRemakes.GameComponents
{
    public class ParallaxedSprite(
        string path, Vector2 initPosition, SpriteBatch batch, float moveSpeed = 100, float scale = 1.0f, int gap = 0, int repeatHalfCount = 1)
        : IGameComponent, IDrawable, ILoadable, IUpdateable
    {
        public int DrawOrder { get; set; } = 0;

        public bool Visible { get; set; } = true;

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 0;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private Texture2D _texture;

        private readonly List<Vector2> _positons = [];
        private float _width = 0;
        private float _movedLength = 0;

        public void Draw(GameTime gameTime)
        {
            batch.Begin();

            for (int i = 0; i < _positons.Count; i++)
            {
                batch.Draw(_texture, _positons[i], null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            }

            batch.End();
        }

        public void Initialize() { }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>(path);
            _width = _texture.Width * scale;

            for (int i = repeatHalfCount; i > 0; i--)
            {
                _positons.Add(initPosition with { X = initPosition.X - i * (_width + gap) });
            }

            _positons.Add(initPosition);

            for (int i = repeatHalfCount; i > 0; i--)
            {
                _positons.Add(initPosition with { X = initPosition.X + i * (_width + gap) });
            }
        }

        public void Update(GameTime gameTime)
        {
            float delta = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _movedLength += delta;
            if (_movedLength >= _width + gap)
            {
                _movedLength = 0;

                var p0 = _positons[0];
                _positons.RemoveAt(0);

                p0.X += (_width + gap) * ((2 * repeatHalfCount) + 1);
                _positons.Add(p0);
            }

            for (int i = 0; i < _positons.Count; i++)
            {
                _positons[i] = _positons[i] with { X = _positons[i].X - delta };
            }
        }
    }
}
