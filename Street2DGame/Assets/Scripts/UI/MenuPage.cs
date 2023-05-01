using DG.Tweening;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

[RequireComponent(typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class MenuPage : UIMovement
{
    [SerializeField]
    private CanvasGroup canvasGroup;
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
    }

    public override void MoveOrigin()
    {
        base.MoveOrigin();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void ResetPosition()
    {
        rectTransform.DOAnchorPos3D(origin, 0.01f).SetUpdate(true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}