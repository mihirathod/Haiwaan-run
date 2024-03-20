using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;

    [Header ("Audio Source")]
    public AudioSource Swipe_AudioSource;
    public AudioSource Hit_AudioSource;
    public AudioSource Destroy_AudioSource;
    public AudioSource Attack_AudioSource;
    public AudioSource Transform_AudioSource;
    public AudioSource Coin_AudioSource;
    public AudioSource Button_AudioSource;

    [Header("Audio Clips")]
    public AudioClip Move_Clip;
    public AudioClip JumpVoice_Clip;
    public AudioClip Slide_Clip;
    public AudioClip Hit_Clip;
    public AudioClip Destroy_Clip;
    public AudioClip Attack_Clip;
    public AudioClip Transform_Clip;
    public AudioClip Coin_Clip;
    public AudioClip PlayButton_Clip;
    public AudioClip NormalButton_Clip;
    public AudioClip PowerCollect_Clip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void PlayMoveSound()
    {
        Swipe_AudioSource.PlayOneShot(Move_Clip);
    }
    public void PlayJumpSound()
    {
        Swipe_AudioSource.PlayOneShot(JumpVoice_Clip);
    }
    public void PlaySlideSound()
    {
        Swipe_AudioSource.PlayOneShot(Slide_Clip);
    }
    public void PlayHitSound()
    {
        GameManager.Instance.PlayDefaultVibration();
        Hit_AudioSource.PlayOneShot(Hit_Clip);
    }
    public void PlayDestroySound()
    {
        GameManager.Instance.PlaySmallVibration();
        Destroy_AudioSource.PlayOneShot(Destroy_Clip);
    }
    public void PlayAttackSound()
    {
        GameManager.Instance.PlaySmallVibration();
        Attack_AudioSource.PlayOneShot(Attack_Clip);
    }
    public void PlayTransformSound()
    {
        GameManager.Instance.PlayDefaultVibration();
        Transform_AudioSource.PlayOneShot(Transform_Clip);
    }
    public void PlayPowerCollectSound()
    {
        Transform_AudioSource.PlayOneShot(PowerCollect_Clip);
    }
    public void PlayCoinSound()
    {
        Coin_AudioSource.PlayOneShot(Coin_Clip);
    }
    public void NormalClickSound()
    {
        Button_AudioSource.PlayOneShot(NormalButton_Clip);
    }
    public void PlayButtonClickSound()
    {
        Button_AudioSource.PlayOneShot(PlayButton_Clip);
    }
}
