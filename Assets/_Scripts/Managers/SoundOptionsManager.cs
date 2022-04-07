using UnityEngine;

public class SoundOptionsManager : MonoBehaviour
{
    // Variables (can be read globally)(Used by the SoundManager)
    public static float musicVolume { get; private set; }
    public static float soundFXVolume { get; private set; }

    // Method: Used in Sounds Menu, controls the music volume
    public void MusicSliderChange(float value)
    {
        musicVolume = value;
        SoundManager.Instance.UpdateMixerVolume();
    }

    // Method: Used in Sounds Menu, controls the sound fx volume
    public void SoundFXSliderChange(float value)
    {
        soundFXVolume = value;
        SoundManager.Instance.UpdateMixerVolume();
    }
}
