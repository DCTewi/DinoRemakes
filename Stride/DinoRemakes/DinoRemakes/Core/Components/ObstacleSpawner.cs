using DinoRemakes.Core.Extensions;
using DinoRemakes.Core.Models;

using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Engine;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DinoRemakes.Core.Components
{
    public class ObstacleSpawner : SyncScript
    {
        public List<UrlReference<Prefab>> Cactuses { get; set; } = [];
        public List<UrlReference<Prefab>> Flyman { get; set; } = [];
        public Vector3 CactusSpawnPosition { get; set; } = Vector3.Zero;
        public Vector3 FlymanSpawnPosition { get; set; } = Vector3.Zero;


        private readonly List<Prefab> _cactusPrefabs = [];
        private readonly List<Prefab> _flymanPrefabs = [];
        private Entity _spawnContainer;

        private float RandomDelayTime => 1.2f + _random.NextSingle() * 0.8f;
        private readonly Random _random = new();

        private float _nextDelayTime = 0f;
        private float _currentDelayTime = 0f;

        public override void Start()
        {
            base.Start();

            foreach (var c in Cactuses)
            {
                _cactusPrefabs.Add(Content.Load(c));
            }
            foreach (var c in Flyman)
            {
                _flymanPrefabs.Add(Content.Load(c));
            }

            _spawnContainer = [];
            Entity.AddChild(_spawnContainer);
        }

        public override void Update()
        {
            if (Globals.State.Paused)
            {
                return;
            }

            var delta = Game.DeltaTime();
            _currentDelayTime += delta;

            if (_currentDelayTime >= _nextDelayTime)
            {
                Spawn();

                _currentDelayTime = 0f;
                _nextDelayTime = RandomDelayTime;
            }

            DebugText.Print($"Obstacles count: {_spawnContainer.GetChildren().Count()}", new Int2(10, 60));
        }

        private void Spawn()
        {
            bool spawnFlyman = _random.NextDouble() < 0.3;

            if (spawnFlyman)
            {
                int index = _random.Next(0, _flymanPrefabs.Count);
                var prefab = _flymanPrefabs[index];
                var entity = prefab.Instantiate().First();
                entity.Transform.Position = FlymanSpawnPosition;
                _spawnContainer.AddChild(entity);
            }
            else
            {
                int index = _random.Next(0, _cactusPrefabs.Count);
                var prefab = _cactusPrefabs[index];
                var entity = prefab.Instantiate().First();
                entity.Transform.Position = CactusSpawnPosition;
                _spawnContainer.AddChild(entity);
            }
        }
    }
}
