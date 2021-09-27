using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIO : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void PLAy()
    {
        source.PlayOneShot(clip);
    }
}
