using DG.Tweening;
using Nocturne.GeneralTools;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadingControl : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private Canvas canvasParent;

    [SerializeField]
    private float fadeTime;

    [SerializeField]
    private bool fadeAtStart = true;

    private void Start()
    {
        if (!fadeImage) fadeAtStart = GetComponent<Image>();

        if (!canvasParent) canvasParent = GetComponentInParent<Canvas>();
        if (fadeAtStart)
        {
            FadeOut();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutSequence());
    }

    private IEnumerator FadeOutSequence()
    {
        fadeImage.DOFade(0f, fadeTime).SetUpdate(true);
        yield return Helpers.GetWaitRealtime(fadeTime);
        canvasParent.enabled = false;
        fadeImage.enabled = false;
    }

    public void FadeIn()
    {
        fadeImage.enabled = true;
        canvasParent.enabled = true;
        fadeImage.DOFade(1f, fadeTime).SetUpdate(true);
    }
}