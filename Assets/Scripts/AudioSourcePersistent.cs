using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSourcePersistent : MonoBehaviour
{
    public static AudioSourcePersistent Instance;
    public GameObject menu, gameplay;
    public AudioSource source;

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = GetComponent<AudioSourcePersistent>();
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void InMenu(bool a)
    {
        print(a);
        menu.SetActive(a);
        gameplay.SetActive(!a);
        source.enabled = false;

    }


}
