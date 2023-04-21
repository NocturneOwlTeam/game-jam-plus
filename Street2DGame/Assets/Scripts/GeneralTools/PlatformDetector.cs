using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    public static PlatformDetector Instance { get; private set; }
    public RuntimePlatform currentPlatform { get; private set; }

    private void Awake()
    {
        Instance = this;
        currentPlatform = Application.platform;
    }

    public bool OnPC => currentPlatform == RuntimePlatform.WindowsPlayer || currentPlatform == RuntimePlatform.OSXPlayer || currentPlatform == RuntimePlatform.LinuxPlayer || currentPlatform == RuntimePlatform.WindowsEditor;

    public bool OnWeb => currentPlatform == RuntimePlatform.WebGLPlayer;
}