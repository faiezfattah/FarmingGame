using TriInspector;
using UnityEngine;

namespace Script.Core.Utils {
    [HideMonoScript]
    public class Logger : MonoBehaviour {
        [SerializeField] bool Show = true;
        [SerializeField] Color PrefixColor = Color.white;
        string GetPrefix => $"<color=#{ColorUtility.ToHtmlStringRGB(PrefixColor)}>[{name}]</color> ";
        public void Log(string message, Object context = null) {
            if (Show)
                Debug.Log(GetPrefix + message, context);
        }
    }
}


