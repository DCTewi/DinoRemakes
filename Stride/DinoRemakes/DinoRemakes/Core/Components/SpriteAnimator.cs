using DinoRemakes.Core.Extensions;
using DinoRemakes.Core.Models;

using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Rendering.Sprites;

using System.Collections.Generic;
using System.Linq;

namespace DinoRemakes.Core.Components
{
    public class SpriteAnimator : SyncScript
    {
        public Dictionary<string, Int2> Animations { get; set; } = [];
        public float FPS { get => 1.0f / _secondPerFrame; set => _secondPerFrame = 1.0f / value; }

        private SpriteComponent _sprite;
        private SpriteFromSheet _sheet;
        private float _secondPerFrame = 0.1f;
        private float _frameTimer = 0f;
        private bool _paused = false;
        private string _currentAnimation = string.Empty;

        public void Pause()
        {
            _paused = true;
        }

        public void Stop()
        {
            _paused = true;
            if (!Animations.TryGetValue(_currentAnimation, out var anim))
            {
                anim = Int2.Zero;
            }

            _sheet.CurrentFrame = anim.X;
        }

        public void Play(string key)
        {
            _currentAnimation = key;
            if (!Animations.TryGetValue(_currentAnimation, out var anim))
            {
                anim = Int2.Zero;
            }
            _sheet.CurrentFrame = anim.X;
            _paused = false;
        }

        public override void Start()
        {
            base.Start();

            _sprite = Entity.Get<SpriteComponent>();
            _sheet = _sprite.SpriteProvider as SpriteFromSheet;

            _currentAnimation = Animations.Keys.First();
        }

        public override void Update()
        {
            if (Globals.State.Paused || _paused)
            {
                return;
            }

            float delta = Game.DeltaTime();
            _frameTimer += delta;

            if (_frameTimer > _secondPerFrame)
            {
                _frameTimer = 0f;

                if (!Animations.TryGetValue(_currentAnimation, out var range))
                {
                    range = new(0, _sheet.SpritesCount - 1);
                }

                var nextFrame = _sprite.CurrentFrame + 1;
                if (nextFrame > range.Y)
                {
                    nextFrame = range.X;
                }

                _sheet.CurrentFrame = nextFrame;
            }
        }
    }
}
