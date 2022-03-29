using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Used to set up a Player preference
    // Used for persistence between scenes
    public const string AudioMutePref = "audioMutePref";

    // Allows for methods of SoundManager to be called acoordingly
    public static SoundManager Instance;

    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixerGroup soundFXGroup;
    [SerializeField] private AudioMixerGroup musicGroup;

    private void Awake()
    {
        Instance = this;

        //SingletonSetup();

        //If a player pref was set for audio mute
        if (PlayerPrefs.HasKey(AudioMutePref))
        {
            // Set audio listener volume to that of the player pref
            AudioListener.volume = PlayerPrefs.GetFloat(AudioMutePref);
        }

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            // Initialize properties of the AudioSource
            // based on values specified in the current Sound instance
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.isLooping;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.volume = s.volume;

            // Determines which mixer group to place the Sound instance in
            if (s.audioType == Sound.TypesAudio.soundFX)
            {
                s.audioSource.outputAudioMixerGroup = soundFXGroup;
            }
            else if (s.audioType == Sound.TypesAudio.music)
            {
                s.audioSource.outputAudioMixerGroup = musicGroup;
            }

            // When audio source is added at runtime, usually playOnAwake won't work
            // This checks if the current Sound instance is set to playOnAwake
            // If true, will manually begin playing 
            if (s.playOnAwake)
            {
                s.audioSource.Play();
            }
        }
    }

    // Used to ensure that only one instance of a SoundManager exists in a given scene
    private void SingletonSetup()
    {
        // If: More than one GameObject contains this script...
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // Destroy current gameObject
            Destroy(gameObject);
        }
        else
        {
            // Persist the gameObject
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayClip(string clipName)
    {
        // Checks list of sounds within the sounds array and returns the sound which matches that of passed in string name
        Sound soundToPlay = Array.Find(sounds, dummySound => dummySound.clipName == clipName);

        if (soundToPlay != null)
        {
            soundToPlay.audioSource.Play();
        }
    }

    public void StopClip(string clipName)
    {
        // Checks list of sounds within the sounds array and returns the sound which matches that of passed in string name
        Sound soundToPlay = Array.Find(sounds, dummySound => dummySound.clipName == clipName);

        if (soundToPlay != null)
        {
            soundToPlay.audioSource.Stop();
        }
    }

    public void ToggleMute()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

        // Set player preference
        PlayerPrefs.SetFloat(AudioMutePref, AudioListener.volume);
    }

    public void UpdateMixerVolume()
    {
        // Decibles work through logarithmic scale, therefore we must convert the volume float to decibles
        musicGroup.audioMixer.SetFloat("Music Volume",Mathf.Log10(SoundOptionsManager.musicVolume) * 20);

        // Decibles work through logarithmic scale, therefore we must convert the volume float to decibles
       soundFXGroup.audioMixer.SetFloat("SoundFX Volume",Mathf.Log10(SoundOptionsManager.soundFXVolume) * 20);
    }

}
