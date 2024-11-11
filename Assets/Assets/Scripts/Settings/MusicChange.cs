using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicChange : MonoBehaviour
{
    public GameObject musicSliderObj;
    public GameObject soundSliderObj;

    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        musicSlider = musicSliderObj.GetComponent<Slider>();
        soundSlider = soundSliderObj.GetComponent<Slider>();

        musicSlider.value = MusicSettings.musicVolume;
        soundSlider.value = MusicSettings.soundVolume;
    }

    void Update()
    {
        MusicSettings.musicVolume = musicSlider.value;
        MusicSettings.soundVolume = soundSlider.value;
    }
}
