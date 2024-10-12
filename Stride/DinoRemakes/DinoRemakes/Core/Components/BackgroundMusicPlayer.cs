using Stride.Audio;
using Stride.Engine;

namespace DinoRemakes.Core.Components
{
    public class BackgroundMusicPlayer : StartupScript
    {
        public Sound BackgroundMusic { get; set; }

        public override void Start()
        {
            base.Start();

            var music = BackgroundMusic.CreateInstance();
            music.IsLooping = true;
            music.Play();
        }
    }
}
