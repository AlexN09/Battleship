using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource AudioSrc => GetComponent<AudioSource>();
    public void PlaySound(AudioClip clip)
    {
       AudioSrc.PlayOneShot(clip); 
    }
} 
