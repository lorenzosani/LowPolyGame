using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickFx;
    
    public void ClickSound()
    {
        audioSource.clip = clickFx;
        audioSource.Play();
    }
}
