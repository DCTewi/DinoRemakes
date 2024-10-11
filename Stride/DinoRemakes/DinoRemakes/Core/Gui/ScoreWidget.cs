using DinoRemakes.Core.Models;

using Stride.Engine;
using Stride.UI.Controls;

namespace DinoRemakes.Core.Gui
{
    public class ScoreWidget : SyncScript
    {
        private UIComponent _ui;
        private TextBlock _scoreLabel;

        public override void Start()
        {
            base.Start();

            _ui = Entity.Get<UIComponent>();
            _ui.Enabled = true;
            _scoreLabel = _ui.Page.RootElement.FindName("ScoreLabel") as TextBlock;
        }

        public override void Update()
        {
            _scoreLabel.Text = $"得分: {Globals.State.Score}";
        }
    }
}
