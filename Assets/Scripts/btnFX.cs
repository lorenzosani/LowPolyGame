using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource fxSource;
    public AudioClip clickFx;
    public AudioClip buildingFx;
    public AudioClip resourcesFx;
    public AudioClip victoryFx;
    public AudioClip attackFx;

    private float volume = 0.5f;

    void Update(){
        musicSource.volume = volume;
        fxSource.volume = volume/5.0f;
    }
    
    // This plays a sound when a button is clicked
    public void ClickSound(){
        fxSource.clip = clickFx;
        fxSource.Play();
    }

    public void BuildingSound(){
        fxSource.clip = buildingFx;
        fxSource.Play();
    }

    public void ResourcesSound(){
        fxSource.clip = resourcesFx;
        fxSource.Play();
    }

    public void VictorySound(){
        fxSource.clip = victoryFx;
        fxSource.Play();
    }

    public void AttackSound(){
        fxSource.clip = attackFx;
        fxSource.Play();
    }

    // This allows the user to specify the desired volume of the sound
    public void SetVolume(float vol){
        volume = vol;
    }
}
