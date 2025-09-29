using System;
namespace OneiricFarming.Core.Utils.EventBinding {
    /// <summary>
    /// for controlling who can raise or trigger events by hiding other method behind this interface.
    /// dont understand? ask fafa.
    /// </summary>
    public interface IEventHook {
        public IDisposable Subscribe(Action action);
    }
    /// <summary>
    /// <inheritdoc cref="IEventHook"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHook<T> {
        public IDisposable Subscribe(Action<T> action);
    }

}
