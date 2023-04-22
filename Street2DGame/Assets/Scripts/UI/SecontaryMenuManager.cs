using UnityEngine;
using UnityEngine.Events;

public class SecontaryMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public bool isInMenu { get; private set; }

    public UnityEvent OnShownMenu;

    public UnityEvent OnHideMenu;

    private void Update()
    {
        if (Input.GetKeyDown("Secontary"))
        {
            ChangeMenu();
        }
    }

    public void ChangeMenu()
    {
        if (!isInMenu)
        {
            ShowSecondaryMenu();
        }
        else
        {
            HideSecondaryMenu();
        }
    }

    public void ShowSecondaryMenu()
    {
        isInMenu = true;
        menu.SetActive(isInMenu);
        OnShownMenu.Invoke();
        //Pause any input whatsoever;
    }

    public void HideSecondaryMenu()
    {
        isInMenu = false;
        menu.SetActive(isInMenu);
        OnHideMenu.Invoke();
        //Resume any input whatsoever;
    }
}