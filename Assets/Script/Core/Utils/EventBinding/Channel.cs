using System;
using UnityEngine;

namespace OneiricFarming.Core.Utils.EventBinding {

    /// <summary>
    /// This class is a wrapper for values that emit events value changed. Internally this uses <see cref="Signal"/>
    /// Use it if you need to track changes within a class. Can be used with <see cref="SubscriptionBag"/>
    /// </summary>
    /// <typeparam name="T">The type of data. Can be int, float, vector, anything really</typeparam>
    [Serializable]
    public struct Channel<T> : IDisposable, IEventHook<T> {
        byte _isDisposed;

        [SerializeField]
        T _value;
        public T Value {
            get {
                if (_isDisposed == 1) throw new ObjectDisposedException("Trying to access disposed ReactiveProperty. Cannot resume because value is discarded.");
                else return _value;
            }
            set {
                if (_isDisposed == 1) throw new ObjectDisposedException("Trying to access disposed ReactiveProperty. Cannot resume because value is discarded.");
                if (!Equals(_value, value)) { // note: if the value is the same. doesnt trigger.
                    _value = value;
                    TriggerChange();
                }
            }
        }
        Signal<T> _subscriber;
        public Channel(T initialValue) {
            _value = initialValue;
            _isDisposed = 0;
            _subscriber = new Signal<T>();
        }
        public IDisposable Subscribe(Action<T> action) {
            if (_isDisposed == 1) throw new ObjectDisposedException("Trying to access disposed ReactiveProperty. Cannot resume because value is discarded.");

            action?.Invoke(Value); // give the first value

            return _subscriber.Subscribe(action);
        }
        void TriggerChange() {
            _subscriber?.Raise(_value);
        }
        public void Dispose() {
            if (_isDisposed == 1) return;
            _value = default(T);
            _subscriber.Dispose();
            _isDisposed = 1;
        }
    }

}