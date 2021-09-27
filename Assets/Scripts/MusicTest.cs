using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Audio;
public class MusicTest : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private AudioSource _source;
    private AudioMixer mixer;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_source.isPlaying)
        {
            source.enabled = true;
        }
    }

}
