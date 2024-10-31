using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MarkUI : MonoBehaviour
{
    [SerializeField] private GameObject OFind;
    [SerializeField] private GameObject OMark;

    [SerializeField] private Button BFind;
    [SerializeField] private Button BMark;

    private void Start()
    {
        BFind.onClick.AddListener(FindButton);
        BMark.onClick.AddListener(MarkButton);
    }

    public void FindButton()
    {
        OFind.SetActive(true);
        OMark.SetActive(false);

        BFind.GetComponent<Image>().color = new Color(0.2941177f, 0.8078432f, 0f);
        BMark.GetComponent<Image>().color = Color.black;
    }

    public void MarkButton()
    {
        OFind.SetActive(false);
        OMark.SetActive(true);

        BFind.GetComponent<Image>().color = Color.black;
        BMark.GetComponent<Image>().color = new Color(0.2941177f, 0.8078432f, 0f);
    }

    public void ExitButton()
    {
        OFind.SetActive(false);
        OMark.SetActive(false);
        BFind.interactable = true;
        BMark.interactable = true;

        OFind.GetComponent<Image>().color = Color.black;
        OFind.GetComponent<Image>().color = Color.black;
    }
}
