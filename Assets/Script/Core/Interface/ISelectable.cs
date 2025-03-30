using UnityEngine;

namespace Script.Core.Interface {
public interface ISelectable {
    public Vector3 GetPointerPosition();
    public void Select();
}
}