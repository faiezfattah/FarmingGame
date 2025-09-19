using UnityEditor;
using UnityEngine;

public class HierarchyContextMenu {
    [MenuItem("GameObject/Create New Logger", false, 10)]
    static void CreateSpecialObject(MenuCommand menuCommand) {
        GameObject go = new GameObject("New Logger");

        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

        go.AddComponent<Script.Core.Utils.Logger>();

        Selection.activeObject = go;
    }
}