using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallpaper : MonoBehaviour
{
    [SerializeField] Button[] buttons;


    public void SelectWallpaper(int i)
    {
        foreach (var button in buttons)
        {
            if(button == buttons[i])
            {
                button.gameObject.GetComponent<Image>().color = Color.white;
                WallpaperPhone.wallpap = i;
            } else
            {
                button.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
