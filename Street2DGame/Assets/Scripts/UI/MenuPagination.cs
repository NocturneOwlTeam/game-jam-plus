using DG.Tweening;
using UnityEngine;

public class MenuPagination : MonoBehaviour
{
    //Esto es para menus paginados: seleccion de niveles, inventarios, otros menus, etc.).
    //NOTA: si es dinamico, convierte los Arrays en Lists.
    [SerializeField]
    private GameObject[] pages;

    [SerializeField]
    private int startingPage = 0;

    [SerializeField]
    private Transform hidePageRight;

    [SerializeField]
    private Transform hidePageLeft;

    private CanvasGroup[] _canvasGroups;

    private RectTransform[] _transforms;

    public int currentPageIndex { get; private set; }

    [SerializeField]
    private Vector2 shownPosition = Vector2.zero;

    private void Start()
    {
        if (pages.Length == 0)
        {
            Debug.LogError($"La lista tiene {pages.Length} paginas. Por favor, asigne aunque sea una.");
        }
        SetPagesComponents();
    }

    private void SetPagesComponents()
    {
        _canvasGroups = new CanvasGroup[pages.Length];
        _transforms = new RectTransform[pages.Length];
        currentPageIndex = startingPage;
        for (var i = 0; i < pages.Length; i++)
        {
            if (!pages[i].TryGetComponent(out _transforms[i]))
            {
                Debug.LogError($"Este sistema de paginacion solo funciona si los objetos son de tipo UI (que contengan RectTransform). El objeto numero {i} no lo es.");
            }

            if (!pages[i].TryGetComponent(out _canvasGroups[i]))
            {
                Debug.LogError($"El objeto numero {i} no contiene un CanvasGroup, que se necesita para esta paginacion.");
            }

            //La pagina principal
            if (i == currentPageIndex)
            {
                PaginationAnimation(currentPageIndex, shownPosition, true);
            }
            //Es de la paginas siguientes
            else if (i > currentPageIndex)
            {
                PaginationAnimation(i, hidePageRight.localPosition, false);
            }
            //Es de la paginas anteriores
            else
            {
                PaginationAnimation(i, hidePageLeft.localPosition, false);
            }
        }
    }

    private void PaginationAnimation(int index, Vector2 destiny, bool isShown)
    {
        float fade = isShown ? 1 : 0;
        _transforms[index].DOAnchorPos(destiny, 0.3f).SetUpdate(true);
        _canvasGroups[index].DOFade(fade, 0.3f).SetUpdate(true);
        _canvasGroups[index].interactable = isShown;
        _canvasGroups[index].blocksRaycasts = isShown;
    }

    private void InstantPagination(int index, Vector2 destiny, bool isShown)
    {
        float fade = isShown ? 1 : 0;
        _transforms[index].anchoredPosition = destiny;
        _canvasGroups[index].alpha = fade;
        _canvasGroups[index].interactable = isShown;
        _canvasGroups[index].blocksRaycasts = isShown;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("Left"))
        {
            MoveToNextPage();
        }

        if (Input.GetKeyDown("Right"))
        {
            MoveToPreviousPage();
        }
    }

    public void MoveToNextPage()
    {
        if (currentPageIndex >= pages.Length - 1) return;
        PaginationAnimation(currentPageIndex, hidePageLeft.localPosition, false);
        currentPageIndex++;
        PaginationAnimation(currentPageIndex, shownPosition, true);
    }

    public void MoveToPreviousPage()
    {
        if (currentPageIndex <= 0) return;
        PaginationAnimation(currentPageIndex, hidePageRight.localPosition, false);
        currentPageIndex--;
        PaginationAnimation(currentPageIndex, shownPosition, true);
    }

    public void MoveToPage(int page)
    {
        //En caso la pagina ingresada se salga de los limites o sea la misma que las que esta ahora
        if (page < 0 || page >= pages.Length || page == currentPageIndex) return;

        if (page > currentPageIndex)
        {
            for (var i = currentPageIndex; i < page; i++)
            {
                PaginationAnimation(i, hidePageLeft.localPosition, false);
            }
        }
        else if (page < currentPageIndex)
        {
            for (var i = currentPageIndex; i > page; i--)
            {
                PaginationAnimation(i, hidePageRight.localPosition, false);
            }
        }
        currentPageIndex = page;
        PaginationAnimation(currentPageIndex, shownPosition, true);
    }

    public void MoveInstantlyToPage(int page)
    {
        if (page < 0 || page >= pages.Length || page == currentPageIndex) return;
        if (page > currentPageIndex)
        {
            for (var i = currentPageIndex; i < page; i++)
            {
                InstantPagination(i, hidePageLeft.localPosition, false);
            }
        }
        else if (page < currentPageIndex)
        {
            for (var i = currentPageIndex; i > page; i--)
            {
                InstantPagination(i, hidePageRight.localPosition, false);
            }
        }
        currentPageIndex = page;
        InstantPagination(currentPageIndex, shownPosition, true);
    }

    public bool atBeginningPages => currentPageIndex <= 0;

    public bool atEndPages => currentPageIndex >= pages.Length - 1;
}