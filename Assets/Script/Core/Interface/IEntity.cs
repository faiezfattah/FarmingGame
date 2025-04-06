using System;
using Script.Core.Model.Item;

namespace Script.Core.Interface {
public interface IEntity<in T>  {
    public void Initialize(T context, Action onPickup);
}
}