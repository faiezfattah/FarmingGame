using System;

namespace OneiricFarming.Core.Utils.EventBinding {
    public static class ActionExtension {
        public static IDisposable Subscribe(this Action action, Action callback) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            action += callback;

            return new EventBinding(() => {
                action -= callback;
            });
        }
        public static IDisposable Subscribe<T>(this Action<T> action, Action<T> callback) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            action += callback;

            return new EventBinding(() => {
                action -= callback;
            });
        }
    }
}