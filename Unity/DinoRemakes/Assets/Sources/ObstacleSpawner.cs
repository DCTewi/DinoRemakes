using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

using UnityEngine;

using Random = UnityEngine.Random;

namespace DinoRemakes
{
    public sealed class ObstacleSpawner : MonoBehaviour
    {
        [field: SerializeField]
        public List<GameObject> FlymanPrefabs { get; set; } = new();

        [field: SerializeField]
        public List<GameObject> CactusPrefabs { get; set; } = new();

        [field: SerializeField]
        public GameObject FlymanSpawnTransform { get; set; }

        [field: SerializeField]
        public GameObject CactusSpawnTransform { get; set; }

        [field: SerializeField]
        public GameObject SpawnGroup { get; set; }

        private double CooldownInterval => Random.Range(_randomInterval.x, _randomInterval.y) + _hardInverval;
        private readonly double _hardInverval = 1200f;
        private readonly Vector2 _randomInterval = new(0f, 800f);
        private Timer _cooldownTimer;
        private bool _cooldown = false;

        private GameState _gameState;

        private void Awake()
        {
            _cooldownTimer = new()
            {
                AutoReset = false,
                Interval = CooldownInterval,
            };
            _cooldownTimer.Elapsed += OnCooldownTimerElapsed;
            _cooldownTimer.Start();
        }

        private void Start()
        {
            GameManager.Instance.GameRestarted += () =>
            {
                // unity 特有的假 null
                foreach (Transform child in SpawnGroup.transform)
                {
                    Destroy(child.gameObject);
                }
            };

            _gameState = GameManager.Instance.GameState;
        }

        private void Update()
        {
            if (!_gameState.GameOver && !_gameState.Paused && _cooldown)
            {
                bool spawnFlyman = Random.Range(0f, 100f) < 30f;

                if (spawnFlyman)
                {
                    int index = Random.Range(0, FlymanPrefabs.Count);
                    Instantiate(FlymanPrefabs[index], FlymanSpawnTransform.transform.position, Quaternion.identity, SpawnGroup.transform);
                }
                else
                {
                    int index = Random.Range(0, CactusPrefabs.Count);
                    Instantiate(CactusPrefabs[index], CactusSpawnTransform.transform.position, Quaternion.identity, SpawnGroup.transform);
                }

                _cooldown = false;
                _cooldownTimer.Interval = CooldownInterval;
                _cooldownTimer.Start();
            }
        }

        private void OnCooldownTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _cooldownTimer.Stop();
            _cooldown = true;
        }
    }
}
