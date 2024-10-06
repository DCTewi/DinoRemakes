using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace DinoRemakes
{
    public sealed class UIController : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI ScoreLabel { get; set; }

        [field: SerializeField]
        public Image MedalImage { get; set; }

        [field: SerializeField]
        public Sprite BronzeSprite { get; set; }

        [field: SerializeField]
        public Sprite SilverSprite { get; set; }

        [field: SerializeField]
        public Sprite GoldSprite { get; set; }

        [field: SerializeField]
        public Button PausedWidget { get; set; }

        [field: SerializeField]
        public GameObject GameOverWidget { get; set; }

        [field: SerializeField]
        public TextMeshProUGUI GameOverScoreLabel { get; set; }

        [field: SerializeField]
        public Button RestartButton { get; set; }

        private GameState _gameState;
        private long _lastScore = 0L;

        private readonly long _silverScore = 100L;
        private readonly long _goldScore = 600L;

        private void Start()
        {
            _gameState = GameManager.Instance.GameState;

            MedalImage.sprite = BronzeSprite;

            InitGameOverWidgets();

            GameManager.Instance.GamePaused += (paused) =>
            {
                PausedWidget.gameObject.SetActive(paused);
            };
            GameManager.Instance.GameOver += () =>
            {
                int bestScore = GameManager.Instance.GetBestScore();
                int currentScore = _gameState.Score;

                GameOverScoreLabel.text = $"本次得分: {currentScore}\n最高得分: {bestScore}";
                GameOverWidget.SetActive(true);
            };
            GameManager.Instance.GameRestarted += InitGameOverWidgets;

            PausedWidget.onClick.AddListener(() =>
            {
                _gameState.Paused = false;
                GameManager.Instance.ApplyPauseState();
            });

            RestartButton.onClick.AddListener(() =>
            {
                GameManager.Instance.RestartGame();
            });

        }

        private void Update()
        {
            if (_lastScore < _silverScore)
            {
                _lastScore = _gameState.Score;
                if (_lastScore >= _silverScore)
                {
                    MedalImage.sprite = SilverSprite;
                }
            }
            else if (_lastScore < _goldScore)
            {
                _lastScore = _gameState.Score;
                if (_lastScore >= _goldScore)
                {
                    MedalImage.sprite = GoldSprite;
                }
            }

            ScoreLabel.text = $"得分: {_gameState.Score}";
        }

        private void InitGameOverWidgets()
        {
            PausedWidget.gameObject.SetActive(false);
            GameOverWidget.SetActive(false);
        }
    }
}
