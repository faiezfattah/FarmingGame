using R3;
using UnityEngine;

namespace Script.Core.Interface {
    // this is interface for mouse interaction
    public interface IActionable {
        public Vector3 GetPointerPosition();
        public void Action(IUseable item);

        public static class Event {
            public static Subject<IActionable> OnPointerHovered = new();
            public static Subject<IActionable> OnPointerExited = new();
        }
    }
}