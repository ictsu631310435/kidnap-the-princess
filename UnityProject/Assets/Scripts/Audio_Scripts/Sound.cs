using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Script for holding a sound and its settings
[Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
