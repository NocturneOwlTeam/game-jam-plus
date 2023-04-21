using Nocturne.GeneralTools;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadSpecificScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void LoadSpecificSceneDelayed(int index)
    {
        StartCoroutine(Load(index));
    }

    private IEnumerator Load(int index)
    {
        yield return Helpers.GetWaitRealtime(0.5f);
        SceneManager.LoadSceneAsync(index);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}