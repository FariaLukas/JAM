using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public KeyCode pauseButton;
    public GameObject pause;

    private void Update()
    {
        PauseOn();
    }
    private void PauseOn()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }
    }
    public void Despause()
    {
        Time.timeScale = 1;
    }
    public void MainMenu(string load)
    {
        SceneManager.LoadScene(load);
    }
    public void Quit()
    {
        Application.Quit();
    }


}
