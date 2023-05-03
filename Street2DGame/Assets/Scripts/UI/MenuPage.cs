using DG.Tweening;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

[RequireComponent(typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class MenuPage : UIMovement
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private bool canFade;

    private void Awake()
    {
        if (!canvasGroup && !TryGetComponent(out canvasGroup))
        {
            Debug.LogError("La pagina necesita");
        }
    }

    public override void MoveDestination()
    {
        base.MoveDestination();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        if (canFade)
        {
            canvasGroup.DOFade(1f, movementTime);
        }
    }

    public override void MoveOrigin()
    {
        base.MoveOrigin();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        if (canFade)
        {
            canvasGroup.DOFade(0f, movementTime);
        }
    }

    public void ResetPosition()
    {
        rectTransform.DOAnchorPos3D(origin, 0.01f).SetUpdate(true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}