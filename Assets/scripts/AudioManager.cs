using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource FlashRunaudioSource;
    public AudioSource FlyRotateFlashRunaudioSource;
    public AudioClip DashSfx;
    public AudioClip LandSfx;
    public AudioClip ShootSfx;
    public bool playOnce;
     
    private void Awake()
    {
        instance = this;
    }
}
