using UnityEditor;
using UnityEngine;

public class ExitApp : MonoBehaviour
{
    public void ExitApllication()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}