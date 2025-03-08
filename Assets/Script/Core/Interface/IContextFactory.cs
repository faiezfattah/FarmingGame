using System;

namespace Script.Core.Interface {
public interface IContextFactory<T> {
    public T CreateContext(Action<T> onContextRemoval);
}
}