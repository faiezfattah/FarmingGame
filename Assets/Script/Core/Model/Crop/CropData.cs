using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Core.Model.Item;
using TriInspector;
using UnityEngine;

namespace Script.Core.Model.Crop {
    [CreateAssetMenu(fileName = "New Crop Data", menuName = "Crop/Data")]
    public class CropData : ScriptableObject {
        [Range(0.1f, 1f)]
        public float ChanceToAdvance = 0.75f;
        public ItemData itemData;

        [TableList, ShowInInspector]
        [ListDrawerSettings(Draggable = true)]
        public List<CropLevelData> CropLevels;

        public CropContext CreateContext() => new CropContext(this);

        [CanBeNull]
        public CropLevelData GetData(int level) =>
             (level < 0 || level >= CropLevels.Count) ? null : CropLevels[level];
    }
}