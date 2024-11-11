using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallpaperPhone : MonoBehaviour
{
    [SerializeField] Sprite[] wallpapers;

    public static int wallpap;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = wallpapers[wallpap];
    }
}
