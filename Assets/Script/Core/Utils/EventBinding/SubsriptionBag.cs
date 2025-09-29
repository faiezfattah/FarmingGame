using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OneiricFarming.Core.Utils.EventBinding { 
    /// <summary>
    /// This is some nice things Fafa made to manage subscription with IDiposeable interface.
    /// Fafa recommends to use Subject and Reactive Property instead of Action to work with this.
    /// </summary>
    public struct SubscriptionBag : IDisposable {
        List<IDisposable> _subcription;
        bool _isDisposed;
        public SubscriptionBag(int capacity = 3) {
            _subcription = new(capacity);
            _isDisposed = false;
        }
        public void Add(IDisposable disposable) {
            if (_isDisposed) {
                Debug.LogWarning("trying to use disposed subscription bag. do not dynamically change subscription with this it may cause race condition problems. if you cant figure out how to fix your problem just contact fafa.");
                disposable.Dispose();
            }
            _subcription.Add(disposable);
        }
        public void Dispose() {
            if (_isDisposed) {
                Debug.LogWarning("trying to use disposed subscription bag. do not dynamically change subscription with this it may cause race condition problems. if you cant figure out how to fix your problem just contact fafa.");
            }

            foreach (var subs in _subcription) {
                subs.Dispose();
            }
            _subcription = null;
            _isDisposed = true;
        }
    }
    public static class IDisposeableExtension {
        public static void AddTo(this IDisposable disposable, ref SubscriptionBag bag) {
            bag.Add(disposable);
        }
    }

    public static class UnityEventExtensions {    
        public static IDisposable SubscribeUnityEvent(this UnityEvent source, Action action) {
            UnityAction listener = () => action?.Invoke();
            source.AddListener(listener);
            return new Disposeable(() => source.RemoveListener(listener));
        }
        
        public static IDisposable SubscribeUnityEvent<T>(this UnityEvent<T> source, Action<T> action) {
            UnityAction<T> listener = (val) => action?.Invoke(val);
            source.AddListener(listener);
            return new Disposeable(() => source.RemoveListener(listener));
        }
    }
    public readonly struct Disposeable : IDisposable {
        readonly Action _disposeAction;
        public Disposeable(Action disposeAction) {
            _disposeAction = disposeAction;
        }
        public void Dispose() {
            _disposeAction.Invoke();
        }
    }
}
