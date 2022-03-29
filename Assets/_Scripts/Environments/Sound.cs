using UnityEngine;


[System.Serializable]
public class Sound
{
    // Used to determine which group to associate with the sound
    // Enum: Can only be one out of specified type
    // This is so we can control the music and sound fx individually 
    public enum TypesAudio {soundFX, music}
    public TypesAudio audioType;

    [HideInInspector] public AudioSource audioSource;
    public AudioClip audioClip;
    public string clipName;
    public bool isLooping;
    public bool playOnAwake;
    [Range(0,1)] public float volume = 0.5f;
}
