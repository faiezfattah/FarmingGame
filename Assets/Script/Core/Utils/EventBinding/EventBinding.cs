using System;
namespace OneiricFarming.Core.Utils.EventBinding {
    public class EventBinding : IDisposable {
        private Action _unsubscribeAction;
        private bool _isDisposed = false;

        public EventBinding(Action unsubscribeAction) {
            _unsubscribeAction = unsubscribeAction;
        }

        public void Dispose() {
            if (_isDisposed) return;

            _unsubscribeAction?.Invoke();
            _unsubscribeAction = null;
            _isDisposed = true;
        }
    }
}