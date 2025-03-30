using Script.Core.Model.Soil;
using UnityEngine;

namespace Script.Feature.Farm.Soil {
public class SoilSystem : MonoBehaviour {
    [SerializeField] private GameObject farmContainer;
    [SerializeField] private SoilData soilData;
    private SoilRegistry _soilRegistry;
    private void Start() {
        var farmTiles = farmContainer.GetComponentsInChildren<Soil>();

        foreach (var tile in farmTiles) {
            var context = soilData.CreateContext()
                                  .SetPosition(tile.transform.position);
            tile.SetContext(context);
        }
    }
}
}