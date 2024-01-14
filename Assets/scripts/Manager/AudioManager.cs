using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource FlashRunaudioSource;
    public AudioSource FlyAudioSource;
    public AudioSource ShaktimanBgm;
    public AudioClip DashSfx;
    public AudioClip LandSfx;
    public AudioClip ShootSfx;
    public AudioClip CollectSfx;
    public AudioClip hurtSfx;
    public AudioClip equipSfx;
    public AudioClip bookSfx;
    public AudioClip explosionSfx;
    public AudioClip clickSfx;
    public AudioClip infoAuraSfx;
    public AudioClip shieldAuraSfx;
    public bool playOnce;
     
    private void Awake()
    {
        instance = this;
    }
}
