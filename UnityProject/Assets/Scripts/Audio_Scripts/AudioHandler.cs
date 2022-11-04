using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public Sound[] sounds;

    // Awake is called when the script intance is being loaded
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            
            s.source.clip = s.audioClip;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound _s = Array.Find(sounds, sound => sound.name == name);
        _s.source.Play();
    }
}
