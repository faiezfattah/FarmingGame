using System;
using UnityEngine;
using UnityEngine.Events;

namespace OneiricFarming.Core.Utils.EventBinding { 

    /// <summary>
    /// Replacement for using Action for event emitters
    /// Use this instead of Action to work with <see cref="SubscriptionBag"/>
    /// Use it if you need to track changes within a class.
    /// </summary>
    [Serializable]
    public class Signal : IDisposable, IEventHook {
        [SerializeField] UnityEvent OnRaise;
        Action _subscriber;
        bool _isDisposed = false;
        public void Raise() {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveSubject. Cannot resume because listener is discarded. Do no reuse this class, it may cause race conditions.");

            _subscriber?.Invoke();
            OnRaise?.Invoke();
        }
        public IDisposable Subscribe(Action action) {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveSubject. Cannot resume because listener is discarded. Do no reuse this class, it may cause race conditions.");

            _subscriber += action;

            return new EventBinding(() => {
                if (!_isDisposed) {
                    _subscriber -= action;
                }
            });
        }
        public void Dispose() {
            Debug.Log("disposed");
            if (_isDisposed) return;
            _subscriber = null;
            _isDisposed = true;
        }
    }
    /// <summary>
    /// <inheritdoc cref="Signal"/>
    /// This generic version can be used if the event needs some argument
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Signal<T> : IDisposable, IEventHook<T> {
        [SerializeField] UnityEvent<T> OnRaise;
        Action<T> _subscriber;
        bool _isDisposed = false;
        public void Raise(T value) {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveSubject. Cannot resume because listener is discarded. Do no reuse this class, it may cause race conditions.");

            OnRaise?.Invoke(value);
            _subscriber?.Invoke(value);
        }
        public IDisposable Subscribe(Action<T> action) {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveSubject. Cannot resume because listener is discarded. Do no reuse this class, it may cause race conditions.");
            
            _subscriber += action;
            
            return new EventBinding(() => {
                if (!_isDisposed) {
                    _subscriber -= action;
                }
            });
        }
        public void Dispose() {
            Debug.Log("disposed");
            if (_isDisposed) return;
            _subscriber = null;
            _isDisposed = true;
            OnRaise.RemoveAllListeners();
        }
    }
}