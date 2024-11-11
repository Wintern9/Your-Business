using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicSettings : MonoBehaviour
{
    public AudioClip soundEffect;
    public AudioSource music;

    static public float musicVolume = 1f;
    static public float soundVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        music = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySoundEffect();
        }

        music.volume = musicVolume;
    }

    void PlaySoundEffect()
    {
        GameObject soundObject = new GameObject("TempSound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        audioSource.clip = soundEffect;
        audioSource.volume = soundVolume;

        audioSource.Play();

        Destroy(soundObject, soundEffect.length);
    }
}
