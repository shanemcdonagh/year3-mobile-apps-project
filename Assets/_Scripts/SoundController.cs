using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures that an AudioSource component will be attached to 
// the associated GameObject
[RequireComponent(typeof(AudioSource))]

public class SoundController : MonoBehaviour
{
    // Objects
    AudioSource audioSource;

    void Start()
    {
        // Retrieve the AudioSource component associated with the GameObject
        audioSource = GetComponent<AudioSource>();
    }
    
    // Plays an audio clip that is passed to it
    public void PlayOneShot(AudioClip clip)
    {
        // If: The clip is not null..
        if(clip)
        {
            // Play audio once
            audioSource.PlayOneShot(clip);
        }
    }

}
