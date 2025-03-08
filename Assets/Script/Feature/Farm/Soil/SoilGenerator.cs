using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using Script.Registry.Farm.Soil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;

namespace Script.Feature.Farm.Soil {
public class SoilGenerator : MonoBehaviour {
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap farmTilemap;
    [SerializeField] private TileBase farmTile;

    [Header("Settings"), SerializeField] 
    private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    private SoilRegistry _soilRegistry;
    private List<Vector3Int> _positions = new();
    private List<TileBase> _tiles = new();

    [Inject]
    public void Construct(SoilRegistry soilRegistry) {
        _soilRegistry = soilRegistry;
        _soilRegistry.SoilContexts.Subscribe(x => UpdateFarmTiles(x));
    }

    private void UpdateFarmTiles(HashSet<SoilContext> soilContexts) {
        Debug.Log(soilContexts.Count);
        var tile = new TileBase[soilContexts.Count];
        Array.Fill(tile, farmTile);
        farmTilemap.SetTiles(soilContexts.Select(x => (Vector3Int) x.Position).ToArray(), tile);
    }

}
}