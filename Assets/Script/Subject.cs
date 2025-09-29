using Reflex.Attributes;
using UnityEngine;

public class Subject : MonoBehaviour {
    [Inject] public string subjectName;
    void Start() {
        Debug.Log("Subject Name: " + subjectName);
    }
}