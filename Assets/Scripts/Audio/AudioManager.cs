using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    [SerializeField]
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() {

        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string _name) {
        /* foreach (Sound s in sounds) {
            if(s.name == _name)
                s.source.Play();
        } */
        
        
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null) {
            Debug.LogWarning("AudioManager: sonido/musica -> " + name + " no encontrado");
            return;
        }
        s.source.Play();
        
    }

    public void Stop(string _name) {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null) {
            Debug.LogWarning("AudioManager: sonido/musica -> " + name + " no encontrado");
            return;
        }
        s.source.Stop();
    }

    public void SetVolume(float volume) {
        
        foreach (Sound s in sounds) {
            s.source.volume = volume;
            if(s.name == "MainTheme")
                Debug.Log("Volume of Main Theme is: " + s.source.volume);
        }

    }
}

// FindObjectOfType<AudioManager>().Play("SoundName");