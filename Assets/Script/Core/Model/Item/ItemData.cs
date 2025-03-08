using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Data")]
public class ItemData : ScriptableObject {
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int sellPrice;

    public ItemContext Create() => new ItemContext() {
        Sprite = itemSprite,
        Price = sellPrice,
    };
}
}