using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseMenu;

    [SerializeField]
    private MenuPage mainMenu;

    [SerializeField]
    private UnityEvent OnPause;

    [SerializeField]
    private UnityEvent OnResume;

    private bool canNavigate = false;

    public bool isPaused { get; private set; }

    [SerializeField]
    public bool canPause;

    protected Stack<MenuPage> menuPages = new Stack<MenuPage>();

    private void Start()
    {
        Time.timeScale = 1;
        menuPages.Push(mainMenu);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && canPause)
        {
            PauseOrNavigate();
        }
    }

    public void PauseOrNavigate()
    {
        canNavigate = menuPages.Count > 1;

        if (!canNavigate)
        {
            SwitchPauseStatus();
        }else if(canNavigate)
        {
            BackToPreviousMenu();
        }
    }

    public void SwitchPauseStatus()
    {
        //Aqui se ponen las demas condiciones adiciones
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
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

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        //Pause any input whatsoever;
        pauseMenu.enabled = isPaused;
        //pauseMenu.SetActive(isPaused);
        mainMenu.MoveDestination();
        OnPause.Invoke();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        //Resume any input whatsoever;
        //pauseMenu.SetActive(isPaused);
        pauseMenu.enabled = isPaused;
        mainMenu.ResetPosition();
        OnResume.Invoke();
    }
}