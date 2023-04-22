using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private UnityEvent OnPause;

    [SerializeField]
    private UnityEvent OnResume;

    public bool isPaused { get; private set; }

    public bool canPause { get; set; }

    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("Cancel") && canPause)
        {
            SwitchPauseStatus();
        }
    }

    public void SwitchPauseStatus()
    {
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        //Pause any input whatsoever;
        pauseMenu.SetActive(isPaused);
        OnPause.Invoke();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        //Resume any input whatsoever;
        pauseMenu.SetActive(isPaused);
        OnResume.Invoke();
    }
}