using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
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
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && this.isPaused)
        {
            Time.timeScale = 1;
            this.pauseMenu.SetActive(false);
            this.isPaused = false;
        }
    }

}
