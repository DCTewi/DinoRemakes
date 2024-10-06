using DinoRemakes.Input;

using System;

using UnityEngine;

namespace DinoRemakes
{
    public sealed class GameManager : MonoBehaviour
    {
        private static readonly string _BestScoreKey = "BestScore";

        public static GameManager Instance;

        public event Action GameRestarted;
        public event Action GameOver;
        public event Action<bool> GamePaused;

        [field: SerializeField]
        public GameState GameState { get; set; } = new();

        public GameInputActions Input { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Input = new();
            Input.Player.Pause.performed += OnPausePerformed;

            Input.Enable();
        }

        private void FixedUpdate()
        {
            GameState.TimeSinceBegin += (double)Time.fixedDeltaTime;
            GameState.Score = (int)(GameState.TimeSinceBegin * 10.0);
            if (GameState.GameSpeed < GameState.MaxGameSpeed)
            {
                // GameSpeed will be MaxGameSpeed after MaxGameSpeedDuration seconds.
                GameState.GameSpeed = Math.Pow(GameState.MaxGameSpeed,
                    GameState.TimeSinceBegin / GameState.MaxGameSpeedDuration);
            }
        }

        public void ApplyPauseState()
        {
            if (GameState.GameOver)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = GameState.Paused ? 0f : 1f;
            }
            GamePaused?.Invoke(GameState.Paused);
        }

        public void SetGameOver()
        {
            GameState.GameOver = true;
            Time.timeScale = 0f;

            if (!PlayerPrefs.HasKey(_BestScoreKey) || PlayerPrefs.GetInt(_BestScoreKey) < GameState.Score)
            {
                PlayerPrefs.SetInt(_BestScoreKey, GameState.Score);
                PlayerPrefs.Save();
            }

            GameOver?.Invoke();
        }

        public int GetBestScore()
        {
            if (PlayerPrefs.HasKey(_BestScoreKey))
            {
                return PlayerPrefs.GetInt(_BestScoreKey);
            }
            return 0;
        }

        public void RestartGame()
        {
            GameState.Reset();
            ApplyPauseState();
            GameRestarted?.Invoke();
        }

        private void OnPausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext cb)
        {
            if (GameState.GameOver)
            {
                return;
            }

            GameState.Paused = !GameState.Paused;
            ApplyPauseState();
        }
    }
}
