using Script.Core.Interface;
using UnityEngine;

namespace Script.Feature.Character.Player {
public class PlayerPointer : MonoBehaviour {
    [SerializeField] private GameObject indicator;
    private bool pointerActive = false;
    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent<ISelectable>(out var selectable);
        if (selectable != null) {
            indicator.SetActive(true);
            indicator.transform.position = selectable.GetPointerPosition();
        }
    }
}
}