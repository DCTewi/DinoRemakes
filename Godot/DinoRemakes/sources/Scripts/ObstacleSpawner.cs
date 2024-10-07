using DinoRemakes.Sources.Autoloads;

using Godot;

namespace DinoRemakes.Sources.Scripts;

public sealed partial class ObstacleSpawner : Node
{
    [Export]
    public Godot.Collections.Array<PackedScene> FlymanScenes;

    [Export]
    public Godot.Collections.Array<PackedScene> CactusScenes;

    private Timer _spawnTimer;
    private Node _spawnContainer;
    private Node2D _flymanTransform;
    private Node2D _cactusTransform;
    private RandomNumberGenerator _random = new();

    private float CooldownInterval => _random.RandfRange(_randomInterval.X, _randomInterval.Y) + _hardInterval;
    private readonly float _hardInterval = 1.2f;
    private readonly Vector2 _randomInterval = new(0f, 0.8f);

    public override void _Ready()
    {
        base._Ready();

        _spawnTimer = GetNode<Timer>("./Timer");
        _flymanTransform = GetNode<Node2D>("./FlymanTransform");
        _cactusTransform = GetNode<Node2D>("./CactusTransform");
        _spawnContainer = new()
        {
            Name = "@SpawnContainer",
        };

        Global.Instance.GameRestarted += OnGameRestarted;

        AddChild(_spawnContainer);

        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.Stop();
    }

    private void OnGameRestarted()
    {
        if (_spawnContainer is Node root)
        {
            while (root.GetChildCount() > 0)
            {
                var node = root.GetChild(0);
                node.QueueFree();
                root.RemoveChild(node);
            }
        }
    }

    private void OnSpawnTimerTimeout()
    {
        bool spawnFlyman = _random.RandfRange(0f, 100f) < 30f;

        if (spawnFlyman)
        {
            int index = _random.RandiRange(0, FlymanScenes.Count - 1);
            var node = FlymanScenes[index].Instantiate<Obstacle>();
            node.Position = _flymanTransform.Position;
            _spawnContainer.AddChild(node);
        }
        else
        {

            int index = _random.RandiRange(0, CactusScenes.Count - 1);
            var node = CactusScenes[index].Instantiate<Obstacle>();
            node.Position = _cactusTransform.Position;
            _spawnContainer.AddChild(node);
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_spawnTimer.IsStopped())
        {
            _spawnTimer.Start(CooldownInterval);
        }
    }
}
