using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public const string MUSIC_VOL = "musicVolume";
    public const string SFX_VOL = "sfxVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    public AudioClip sound1, sound2, sound3, sound4;

    private int scene;

    private void Awake()
    {
        // If there is an instance, and it's not this, delete myself. --> Singleton

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //start playing at the begining of the game. 
        //after this, make that depending of the scene (sceneIndex.scene) the background music will change
        //en tenir resolt lo de s'inicialització dels scripts faré aquí a s'start es switch de ses escenes
        RefreshSceneMusic();
    }

    private void Update()
    {
        if (scene != SceneManager.GetActiveScene().buildIndex)//when the scene is changed, refresh the music
        {
            RefreshSceneMusic();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void RefreshSceneMusic()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        switch (scene)
        {
            case (int)SceneIndex.MainMenu:
                //play background music 1
                musicAudioSource.clip = sound1;
                musicAudioSource.loop = true;
                musicAudioSource.Play();
                break;
            case (int)SceneIndex.GamePlay:
                musicAudioSource.clip = sound2;
                musicAudioSource.loop = true;
                musicAudioSource.Play();
                break;
            case (int)SceneIndex.House:
                //play background music 3
                musicAudioSource.clip = sound1;
                musicAudioSource.loop = true;
                musicAudioSource.Play();
                break;
        }
    }

    public void SetVolumeValue()
    {
        musicAudioSource.volume = PlayerPrefs.GetFloat(MUSIC_VOL);
        sfxAudioSource.volume = PlayerPrefs.GetFloat(SFX_VOL);
    }
}
