using System;
using Script.Core.Interface;
using UnityEngine;

namespace Script.Core.Model.Item {
// [CreateAssetMenu(fileName = "New Tools Data", menuName = "Item/Tools")]
public class ToolData : ItemData<ToolContext> {
    public override ToolContext CreateContext() => new ToolContext(this);
}
}