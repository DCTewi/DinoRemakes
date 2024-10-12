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
        private TextBlock _currentScoreLabel;
        private TextBlock _bestScoreLabel;

        public override async Task Execute()
        {
            _ui = Entity.Get<UIComponent>();
            _ui.Enabled = false;
            _restartButton = _ui.Page.RootElement.FindName("RestartButton") as Button;
            _restartButton.Click += OnRestartButtonClicked;

            _currentScoreLabel = _ui.Page.RootElement.FindName("CurrentScoreLabel") as TextBlock;
            _bestScoreLabel = _ui.Page.RootElement.FindName("BestScoreLabel") as TextBlock;
            while (Game.IsRunning)
            {
                await _gameOverListener.ReceiveAsync();

                var score = Globals.State.Score;
                var save = Globals.Save;

                if (score > save.BestScore)
                {
                    save.BestScore = score;
                    Globals.Save = save;
                }

                var bestScore = save.BestScore;

                _currentScoreLabel.Text = $"本次得分: {score}";
                _bestScoreLabel.Text = $"最高得分: {bestScore}";

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
