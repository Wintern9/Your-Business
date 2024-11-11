using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomItemController : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Button CreateButton;
    [SerializeField] private TMP_Dropdown DropdownTypeItem;
    [SerializeField] private TMP_InputField InputFieldNameItem;
    [SerializeField] private Slider RColor;
    [SerializeField] private Slider GColor;
    [SerializeField] private Slider BColor;

    void Start()
    {
        _material.color = new Color(0f,0f,0f);

        CreateButton.onClick.AddListener(CreateItem);
    }

    void Update()
    {
        _material.color = new Color(RColor.value, GColor.value, BColor.value);
    }

    void CreateItem()
    {
        DBValues.CustomItem =
            new CustomItem(
                ItemName: InputFieldNameItem.name,
                Material: _material,
                ItemType: ItemType.drink
                );
    }
}
