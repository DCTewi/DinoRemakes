using DinoRemakes.Core.Models;

using Stride.Engine;
using Stride.Engine.Events;
using Stride.UI.Controls;

using System.Threading.Tasks;

namespace DinoRemakes.Core.Gui
{
    public class PauseWidget : AsyncScript
    {
        private readonly EventReceiver<bool> _gamePauseListener = new(Globals.GamePausedEventKey);

        private UIComponent _ui;
        private Button _pauseButton;

        public override async Task Execute()
        {
            _ui = Entity.Get<UIComponent>();
            _ui.Enabled = false;
            _pauseButton = _ui.Page.RootElement.FindName("PauseButton") as Button;
            _pauseButton.Click += OnPauseButtonClicked;

            while (Game.IsRunning)
            {
                var paused = await _gamePauseListener.ReceiveAsync();
                _ui.Enabled = paused;
            }
        }

        private void OnPauseButtonClicked(object sender, Stride.UI.Events.RoutedEventArgs e)
        {
            Globals.State.Paused = false;
            Globals.GamePausedEventKey.Broadcast(false);
        }
    }
}
