using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Core.Model.Item;
using Script.Core.Model.Soil;
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
        public List<CropLevelData> cropLevels;

        public CropContext CreateContext() => new CropContext(this);

        [CanBeNull]
        public CropLevelData GetData(int level) =>
             (level < 0 || level >= cropLevels.Count) ? null : cropLevels[level];
        public bool CanHarvest(CropContext context) =>
            context.Level.Value == cropLevels.Count - 1;
        public bool CanAdvance(CropContext context) =>
            ChanceToAdvance > UnityEngine.Random.Range(0, 1f) && context.SoilContext.State.Value == SoilState.Watered;
        public bool ShouldAdvanceToNextLevel(CropContext context) {
            if (context.Level.Value >= cropLevels.Count - 1) {
                return false;
            }
            var nextLevelThreshold = cropLevels[context.Level.Value + 1].GrowthLevel;
            return context.Growth.Value >= nextLevelThreshold;
        }
    }
}