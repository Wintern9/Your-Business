using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class CurrentTime : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMeshProUGUI.text = $"{DateTime.Now.ToString("HH:mm")}";
    }
}
