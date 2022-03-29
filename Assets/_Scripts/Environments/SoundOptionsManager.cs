using UnityEngine;

public class SoundOptionsManager : MonoBehaviour
{
   public static float musicVolume {get; private set;}
   public static float soundFXVolume {get; private set;}

   public void MusicSliderChange(float value)
   {
       musicVolume = value;
       SoundManager.Instance.UpdateMixerVolume();
   }

   public void SoundFXSliderChange(float value)
   {
       soundFXVolume = value;
       SoundManager.Instance.UpdateMixerVolume();
   }
}
