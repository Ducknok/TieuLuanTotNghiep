using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    protected virtual void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Main Menu")
        {
            AudioManager.Instance.backgroundMusic.Stop();
            AudioManager.Instance.PlayAudio(AudioManager.Instance.mainMenu);
        }
        Time.timeScale = 1;
    }
    public void Play()
    {

        SceneManager.LoadScene("Map1");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
