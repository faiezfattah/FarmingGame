using System;
using UnityEngine;
namespace OneiricFarming.Core.Utils.EventBinding {
    public class EventBinding : IDisposable {
        private Action _unsubscribeAction;
        private bool _isDisposed = false;

        internal EventBinding(Action unsubscribeAction) {
            _unsubscribeAction = unsubscribeAction;
        }
        public EventBinding(Action subscribeAction, Action unsubscribeAction) {
            subscribeAction?.Invoke();
            _unsubscribeAction = unsubscribeAction;
        }

        public void Dispose() {
            if (_isDisposed) return;

            _unsubscribeAction?.Invoke();
            _unsubscribeAction = null;
            _isDisposed = true;
        }
    }
    public static class EventBindingExtensions {
        public static EventBinding Create<T>(this EventBinding binding, T ctx, Action<T> subscribeAction, Action unsubscribeAction) {
            subscribeAction?.Invoke(ctx);
            return new EventBinding(unsubscribeAction);
        }
    }
}