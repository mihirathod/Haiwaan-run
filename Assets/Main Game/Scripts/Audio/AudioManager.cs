using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region Variable
    public Slider backgroundSlider, soundEffectsSlider;
    public float backgroundFloat, soundeffectFloat;
    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectAudio;
    private static readonly string defaultPlay = "DefaultPlay";
    int defaultPlayInt;
    #endregion

    #region Function
    void Awake()
    {
        defaultPlayInt = PlayerPrefs.GetInt(defaultPlay);
        CheckDefaultSound();
        UpdateMusic();
    }

    void CheckDefaultSound()
    {
        if (defaultPlayInt == 0)
        {
            backgroundFloat = 0.07f;
            soundeffectFloat = 0.25f;
            backgroundSlider.value = backgroundFloat * 7;
            soundEffectsSlider.value = soundeffectFloat * 2;
            backgroundFloat = PlayerPrefs.GetFloat("BackgroundPref") / 7;
            soundeffectFloat = PlayerPrefs.GetFloat("SoundEffectsPref") / 2;
            PlayerPrefs.SetInt(defaultPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat("BackgroundPref") / 7;
            soundeffectFloat = PlayerPrefs.GetFloat("SoundEffectsPref") / 2;
            backgroundSlider.value = backgroundFloat * 7;
            soundEffectsSlider.value = soundeffectFloat * 2;
        }
    }

    public void UpdateMusic()
    {
        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].volume = backgroundSlider.value / 7;
        }
        //SkillzCrossPlatform.setSkillzMusicVolume(backgroundSlider.value);
        for (int i = 0; i < soundEffectAudio.Length; i++)
        {
            soundEffectAudio[i].volume = soundEffectsSlider.value / 2;
           // SkillzCrossPlatform.setSFXVolume(soundEffectsSlider.value);
        }
        PlayerPrefs.SetFloat("BackgroundPref", backgroundSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsPref", soundEffectsSlider.value);
    }

    public void MuteVolume(bool status)
    {
        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].mute = status;
        }
    }

    public void MuteSFX(bool status)
    {
        for (int i = 0; i < soundEffectAudio.Length; i++)
        {
            soundEffectAudio[i].mute = status;
        }
    }

    #endregion
}


