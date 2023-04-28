using UnityEngine;

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
}