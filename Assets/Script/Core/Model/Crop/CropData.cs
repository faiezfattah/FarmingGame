using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Core.Interface;
using Script.Core.Model.Item;
using TriInspector;
using UnityEngine;

namespace Script.Core.Model.Crop {
[CreateAssetMenu(fileName = "New Crop Data", menuName = "Crop/Data")]
public class CropData : ScriptableObject, IContextFactory<CropContext> {
    public ItemData itemData;
    
    [TableList, ShowInInspector] [ListDrawerSettings(Draggable = true)]
    public List<CropLevelData> cropLevels;

    public CropContext CreateContext(Action<CropContext> onHarvest) => new CropContext(this, onHarvest);
    [CanBeNull] public CropLevelData GetData(int level) =>
         (level < 0 || level >= cropLevels.Count) ? null : cropLevels[level];
    public bool CanHarvest(int currentStateIndex) =>
        currentStateIndex == cropLevels.Count - 1;


    public bool ShouldAdvanceToNextLevel(CropContext context) {
        if (context.Level.Value >= cropLevels.Count - 1) {
            return false;
        }
        var nextLevelThreshold = cropLevels[context.Level.Value + 1].GrowthLevel;
        return context.Growth.Value >= nextLevelThreshold;
    }
}
}