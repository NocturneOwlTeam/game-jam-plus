using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIScaling : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Vector3 scalation;

    [SerializeField]
    private float scaleTime = 0.4f;

    [SerializeField]
    private bool OnStart = false;

    [SerializeField]
    Ease easeDestination = Ease.OutQuart;

    [SerializeField]
    Ease easeOrigin = Ease.OutQuart;

    private Vector3 origin;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        origin = rectTransform.localScale;
        if (OnStart) ScaleDestination();
    }

    public void ScaleOrigin()
    {
        rectTransform.DOScale(origin, scaleTime).SetUpdate(true).SetEase(easeOrigin);
    }

    public void ScaleDestination()
    {
        rectTransform.DOScale(scalation, scaleTime).SetUpdate(true).SetEase(easeDestination);
    }
}
