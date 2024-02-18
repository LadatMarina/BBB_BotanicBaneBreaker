using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    public AudioClip sound1, sound2, sound3, sound4;

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
        }
    }

    private void Start()
    {
        //start playing at the begining of the game. 
        //after this, make that depending of the scene (sceneIndex.scene) the background music will change
        //en tenir resolt lo de s'inicialització dels scripts faré aquí a s'start es switch de ses escenes
        int scene = SceneManager.GetActiveScene().buildIndex;
        switch (scene)
        {
            case (int)SceneIndex.MainMenu:
                //play background music 1
                break;
            case (int)SceneIndex.GamePlay:
                //play background music 2
                break;
            case (int)SceneIndex.Villagers:
                //play background music 3
                break;
            case (int)SceneIndex.Witch:
                //play background music 4
                break;
        }

        musicAudioSource.clip = sound1;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

}
