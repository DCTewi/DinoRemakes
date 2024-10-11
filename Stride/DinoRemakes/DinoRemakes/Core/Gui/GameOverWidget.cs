using DinoRemakes.Core.Models;

using Stride.Engine;
using Stride.Engine.Events;
using Stride.UI.Controls;

using System.Threading.Tasks;

namespace DinoRemakes.Core.Gui
{
    public class GameOverWidget : AsyncScript
    {
        private readonly EventReceiver _gameOverListener = new(Globals.GameOverEventKey);

        private UIComponent _ui;
        private Button _restartButton;

        public override async Task Execute()
        {
            _ui = Entity.Get<UIComponent>();
            _ui.Enabled = false;
            _restartButton = _ui.Page.RootElement.FindName("RestartButton") as Button;
            _restartButton.Click += OnRestartButtonClicked;

            while (Game.IsRunning)
            {
                await _gameOverListener.ReceiveAsync();
                _ui.Enabled = true;
            }
        }

        private void OnRestartButtonClicked(object sender, Stride.UI.Events.RoutedEventArgs e)
        {
            _ui.Enabled = false;
            Globals.GameRestartEventKey.Broadcast();
        }
    }
}
