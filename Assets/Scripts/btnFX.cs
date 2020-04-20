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

    // This plays a sound when a building is placed
    public void BuildingSound(){
        fxSource.clip = buildingFx;
        fxSource.Play();
    }

    // This plays a sound when a resoruce is collected
    public void ResourcesSound(){
        fxSource.clip = resourcesFx;
        fxSource.Play();
    }

    // This plays a sound when a fight is won
    public void VictorySound(){
        fxSource.clip = victoryFx;
        fxSource.Play();
    }

    // This plays a sound when a ship is attacking the village
    public void AttackSound(){
        fxSource.clip = attackFx;
        fxSource.Play();
    }

    // This allows the user to specify the desired volume of the sound
    public void SetVolume(float vol){
        volume = vol;
    }
}
