using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource clickSource;
    public AudioClip clickFx;

    private float volume = 0.5f;

    void Update(){
        musicSource.volume = volume;
        clickSource.volume = volume;
    }
    
    // This plays a sound when a button is clicked
    public void ClickSound()
    {
        clickSource.clip = clickFx;
        clickSource.Play();
    }

    // This allows the user to specify the desired volume of the sound
    public void SetVolume(float vol){
        volume = vol;
    }
}
