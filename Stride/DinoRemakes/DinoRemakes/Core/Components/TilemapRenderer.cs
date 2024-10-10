using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Engine;
using Stride.Graphics;
using Stride.Rendering.Sprites;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DinoRemakes.Core.Components
{
    public class TilemapRenderer : StartupScript
    {
        public UrlReference<SpriteSheet> Tileset { get; set; }
        public UrlReference Tilemap { get; set; }
        public Vector2 TileSize { get; set; } = Vector2.One;
        public Vector2 PixelPerUnit { get; set; } = new(70, 70);
        public Vector3 Anchor { get; set; } = new(-10, 0, 0);

        private SpriteSheet _sheet;
        private List<List<int>> _map;

        public override async void Start()
        {
            base.Start();

            _sheet = await Content.LoadAsync(Tileset);
            _map = await LoadTilemapAsync();

            _sheet.Sprites.ForEach(sprite => sprite.PixelsPerUnit = PixelPerUnit);

            BuildTileTree();
        }

        private void BuildTileTree()
        {
            Vector3 currentPosition = Anchor;

            foreach (var line in _map)
            {
                foreach (var tileId in line)
                {
                    if (tileId > 0)
                    {
                        var index = Math.Clamp(tileId - 1, 0, _sheet.Sprites.Count - 1);

                        var spriteComponent = new SpriteComponent
                        {
                            SpriteProvider = new SpriteFromSheet()
                            {
                                Sheet = _sheet,
                                CurrentFrame = index,
                            }
                        };

                        var entity = new Entity(position: currentPosition)
                        {
                            spriteComponent,
                        };

                        Entity.AddChild(entity);
                    }

                    currentPosition.X += TileSize.X;
                }
                currentPosition.X = Anchor.X;
                currentPosition.Y -= TileSize.Y;
            }
        }

        private async Task<List<List<int>>> LoadTilemapAsync()
        {
            var result = new List<List<int>>();

            using var stream = Content.OpenAsStream(Tilemap);
            using var reader = new StreamReader(stream);

            var csv = await reader.ReadToEndAsync();

            foreach (var line in csv.Split('\n'))
            {
                var tileLine = new List<int>();
                foreach (var str in line.Trim().Split(','))
                {
                    if (int.TryParse(str, out int tileId))
                    {
                        tileLine.Add(tileId);
                    }
                }
                result.Add(tileLine);
            }

            return result;
        }
    }
}
