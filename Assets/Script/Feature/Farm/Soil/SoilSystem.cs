using System;
using System.Linq;
using Script.Registry.Farm.Soil;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;

namespace Script.Feature.Farm.Soil {
public class SoilSystem {
    private Vector2Int startPos = new (-9, 1);
    private Vector2Int endPos = new (-2, -2);
    private SoilRegistry _soilRegistry;

    public SoilSystem(SoilRegistry soilRegistry) {
        _soilRegistry = soilRegistry;
        GenerateFarm();
    }
    public void AddTiles(Vector3Int[] positions, TileBase tile) {
        var tileArr = new TileBase[positions.Length];
        Array.Fill(tileArr, tile);

        var contexts = new SoilContext[positions.Length];

        for (var i = 0; i < positions.Length; i++) {
            contexts[i] = new SoilContext((Vector2Int) positions[i]);
            _soilRegistry.TryAdd(contexts[i]);
        }
    }
    // public void GenerateContext(Vector3Int[] positions) {
    //     for (var i = 0; i < positions.Length; i++) {
    //         _soilRegistry.TryAdd(new SoilContext((Vector2Int)positions[i]));
    //     }
    // }
    
    private void GenerateFarm() {
        var minX = Mathf.Min(startPos.x, endPos.x);
        var maxX = Mathf.Max(startPos.x, endPos.x);
        var minY = Mathf.Min(startPos.y, endPos.y);
        var maxY = Mathf.Max(startPos.y, endPos.y);

        for (var i = minX; i <= maxX; i++) {
            for (var j = minY; j <= maxY; j++) {
                _soilRegistry.TryAdd(new SoilContext(new Vector2Int(i, j)));
            }
        }
    }

    // private void GenerateSoil() {
    //     farmTilemap.ClearAllTiles();
    //     farmTilemap.SetTiles(_positions.ToArray(), _tiles.ToArray());
    // }
}
}