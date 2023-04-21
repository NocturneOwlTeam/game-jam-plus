using UnityEngine;

//TODO: moverlo todo a una clase settings.
public class FrameRate : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
