using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUS : MonoBehaviour
{
    public bool inMenu;

    private void Start()
    {
        AudioSourcePersistent.Instance?.InMenu(inMenu);
    }
}
