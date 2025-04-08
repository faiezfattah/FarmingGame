using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using TriInspector;
using UnityEngine;
using VContainer;
namespace Script.Feature.Item {
public class ItemDebug : MonoBehaviour {
    [SerializeField] private ItemData itemToSpawn;
    [SerializeField] private Transform pointer;
    private IItemSystem _itemSystem;
    [Inject] public void Construct(IItemSystem itemSystem) {
        _itemSystem = itemSystem;
    }
    [Button] private void Spawn() {
        _itemSystem.SpawnItem(itemToSpawn, pointer.position);
    }
}
}