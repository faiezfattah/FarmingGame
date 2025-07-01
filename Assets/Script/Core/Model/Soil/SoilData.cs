using System;
using UnityEngine;

namespace Script.Core.Model.Soil {
    [CreateAssetMenu(fileName = "New Soil Data", menuName = "Soil/Data")]
    public class SoilData : ScriptableObject {
        public Mesh initial;
        public Mesh tilled;
        public Mesh watered;
        [SerializeField, Range(0,1f)] private float soilUnwateredChance = 0.5f;

        public SoilContext CreateContext() => new(this);
        public bool ShouldUnwatered(SoilContext soilContext) =>
            UnityEngine.Random.Range(0, 1f) > soilUnwateredChance;   
    }
}