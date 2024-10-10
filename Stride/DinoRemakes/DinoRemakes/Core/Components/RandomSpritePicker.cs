using Stride.Engine;
using Stride.Rendering.Sprites;

using System;

namespace DinoRemakes.Core.Components
{
    public class RandomSpritePicker : StartupScript
    {
        private readonly Random _random = new();
        private SpriteComponent _sprite;
        public override void Start()
        {
            base.Start();

            _sprite = Entity.Get<SpriteComponent>();

            if (_sprite?.SpriteProvider is SpriteFromSheet sheet)
            {
                sheet.CurrentFrame = _random.Next(sheet.SpritesCount);
            }
        }
    }
}
