using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] protected Animator continueAC;
    [SerializeField] protected Animator goToMenuAC;
    [SerializeField] protected GameObject pauseMenu;
    [SerializeField] protected bool isPaused;
    // Start is called before the first frame update
    protected virtual  void Awake()
    {
        Time.timeScale = 1;
        this.pauseMenu.SetActive(false);
        this.isPaused = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.Pause();
        
    }

    public virtual void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !this.isPaused)
        {
            Time.timeScale = 0;
            
            this.pauseMenu.SetActive(true);
            this.isPaused = true;

            this.continueAC.updateMode = AnimatorUpdateMode.UnscaledTime;
            this.goToMenuAC.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && this.isPaused)
        {
            Time.timeScale = 1;
            this.pauseMenu.SetActive(false);
            this.isPaused = false;
        }
    }
    public virtual void Continue()
    {
        Time.timeScale = 1;
        this.pauseMenu.SetActive(false);
        this.isPaused = false;
    }
    public virtual void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 0;
        this.pauseMenu.SetActive(true);
        this.isPaused = true;
    }
}
