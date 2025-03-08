using System.Collections.Generic;

namespace Script.Core.Base {
public interface IRegistry<T> {
    public T TryGet(string id);
    public bool TryAdd(T value);
    public bool TryRemove(T value);
    public IEnumerable<T> GetAll();
}
}