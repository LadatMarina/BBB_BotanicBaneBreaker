 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferences_UI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public const string MUSIC_VOL = "musicVolume";
    public const string SFX_VOL = "sfxVolume";

    private float musicVolume = 0;
    private float sfxVolume = 0;

    // Start is called before the first frame update
    void Start()
    {
        RefreshValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            PlayerPrefs.DeleteAll();
            SetDefaultValues();
            RefreshValues();
        }
    }
    public void SetMusicValue()
    {
        PlayerPrefs.SetFloat(MUSIC_VOL, musicSlider.value);
        SoundManager.Instance.SetVolumeValue();
    }
    public void SetSFXValue()
    {
        PlayerPrefs.SetFloat(SFX_VOL, sfxSlider.value);
        SoundManager.Instance.SetVolumeValue();
    }
    public void RefreshValues()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOL,musicVolume); //if is nothing stored returns the musicVolume value

        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOL,sfxVolume);
    }

    private void SetDefaultValues()
    {
        PlayerPrefs.SetFloat(MUSIC_VOL, 1);
        PlayerPrefs.SetFloat(SFX_VOL, 1);
    }
}
