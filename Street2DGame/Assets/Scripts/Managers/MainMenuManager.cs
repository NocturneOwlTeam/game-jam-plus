using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    protected Stack<MenuPage> menuPages = new Stack<MenuPage>();

    [SerializeField]
    private MenuPage mainMenu;

    [SerializeField]
    private MenuPage exitMenu;

    [SerializeField]
    private bool canNavigate = true;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
         Debug.unityLogger.logEnabled = false;
#endif
    }

    private void Start()
    {
        menuPages.Push(exitMenu);
        menuPages.Push(mainMenu);
        mainMenu.MoveDestination();
        Time.timeScale = 1;
    }

    public void SwitchNavigation(bool change) => canNavigate = change;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && canNavigate)
        {
            //Remove the stack
            if(menuPages.Peek() == exitMenu)
            {
                ExitToMainMenu();
            }
            else
            {
                BackToPreviousMenu();
            }
        }
    }

    public void NextMenu(MenuPage page)
    {
        menuPages.Peek().MoveOrigin();
        menuPages.Push(page);
        page.MoveDestination();
    }

    public void BackToPreviousMenu()
    {
        //Aqui se llama a la animacion de ocultar la pagina actual y llamar a la anterior.
        menuPages.Pop().MoveOrigin();
        menuPages.Peek().MoveDestination();
    }

    public void ExitToMainMenu()
    {
        exitMenu.MoveOrigin();
        menuPages.Push(mainMenu);
        mainMenu.MoveDestination();
    }

}