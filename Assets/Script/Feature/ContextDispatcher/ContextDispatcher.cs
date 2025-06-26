// using System;
// using System.Collections.Generic;
// using Script.Core.Interface.Systems;
// using Script.Core.Model.Item;

// public class ContextDispatcher {
//     Dictionary<Type, IContextFactory> _factory = new();
//     public ContextDispatcher(IItemContextFactory itemContextFactory) {
//         _factory.Add(typeof(ItemContext), itemContextFactory);
//     }
//     public T Create<T>() {
//         return T switch {
//             ItemContext ic => 
//         }
//     }
// }
// public interface IContextFactory{}