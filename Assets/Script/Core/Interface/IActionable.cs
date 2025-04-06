using UnityEngine;

namespace Script.Core.Interface {
public interface IActionable {
    public Vector3 GetPointerPosition();
    public void Action(IUseable item);
}
}