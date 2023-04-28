using DG.Tweening;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Vector3 destination;

    [SerializeField]
    private Vector3 secondDestination;

    [SerializeField]
    private float movementTime = 0.4f;

    private Vector3 origin;

    [SerializeField]
    private bool OnStart = false;

    [SerializeField]
    Ease easeOrigin = Ease.OutQuart;

    [SerializeField]
    Ease easeDestination = Ease.OutQuart;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        origin = rectTransform.anchoredPosition3D;
        if (OnStart) MoveDestination();
    }

    public virtual void MoveDestination()
    {
        rectTransform.DOAnchorPos3D(destination, movementTime).SetUpdate(true).SetEase(easeDestination);
    }
    public virtual void MoveSecondDestination()
    {
        rectTransform.DOAnchorPos3D(secondDestination, movementTime).SetUpdate(true).SetEase(easeDestination);
    }

    public virtual void MoveOrigin()
    {
        rectTransform.DOAnchorPos3D(origin, movementTime).SetUpdate(true).SetEase(easeOrigin);
    }
}