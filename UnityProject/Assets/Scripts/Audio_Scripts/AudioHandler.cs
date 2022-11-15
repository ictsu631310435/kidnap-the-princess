using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public Sound[] sounds;

    private int currentSoundIndex;

    // Awake is called when the script intance is being loaded
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            
            s.source.clip = s.audioClip;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        currentSoundIndex = 0;
    }

    public void Play(string name)
    {
        Sound _s = Array.Find(sounds, sound => sound.name == name);
        _s.source.Play();
    }

    public void PlayFirstSound()
    {
        sounds[0].source.Play();
    }

    public void PlayNextSound()
    {
        sounds[currentSoundIndex].source.Stop();

        currentSoundIndex++;
        sounds[currentSoundIndex].source.Play();
    }
}
