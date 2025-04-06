using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Core.Interface.Systems {
public interface IItemSystem {
    public void SpawnItem(ItemData itemData, Vector3 position);
}
}