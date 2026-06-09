using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer audioMixer; // Reference to your Audio Mixer

    private static AudioManager instance;

    private bool isMuted;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent<AudioManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        Debug.Log("AudioManager Awake");

        // Ensure only one AudioManager instance is active.
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying duplicate AudioManager");
            Destroy(this.gameObject);
            return;
        }

        if (sounds == null)
        {
            Debug.LogError("Sounds array in AudioManager is not assigned.");
            return;
        }

        if (instance == null)
        {
            // If this is the first instance, set it as the instance.
            instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                if (s == null)
                {
                    Debug.LogWarning("One of the sounds in the AudioManager's sounds array is null.");
                    continue;
                }

                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.source != null)
                {
                    s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
                }
            }

            Sound themeSound = System.Array.Find(sounds, sound => sound.name == "Theme");
            if (themeSound != null && themeSound.source != null)
            {
                themeSound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            }           
        }
    }

    private void Start()
    {
        // Load mute state from PlayerPrefs
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        // Play the theme
        Play("Theme");

        float initialVolume = isMuted ? -80f : 0f;
        SetMusicVolume(initialVolume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }

        s.source.Play();
        Debug.Log(name + " played");
    }
}
