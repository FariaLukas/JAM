using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Change(string a)
    {
        SceneManager.LoadScene(a);
        GameManager.Instance?.OnChange?.Invoke();
    }
    public void Change(int a)
    {
        SceneManager.LoadScene(a);
        GameManager.Instance?.OnChange?.Invoke();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance?.OnChange?.Invoke();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif

    }
}
