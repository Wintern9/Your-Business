using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadOnViewCredit : MonoBehaviour
{
    /// <summary>
    /// Префаб одной из строчек таблицы кредитов
    /// </summary>
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject ContentObject;

    private void Start()
    {
        InstantiateTable();
    }

    public void UpdateView()
    {
        foreach (Transform item in ContentObject.transform)
            Destroy(item.gameObject);
        InstantiateTable();
    }

    void InstantiateTable()
    {
        float PosY = 0f;
        Debug.Log(DBValues.Credit.Count);
        for (int i = 0; i < DBValues.Credit.Count; i++)
        {
            Debug.Log(DBValues.Credit[i]);

            GameObject obj = Instantiate(item);
            obj.transform.SetParent(ContentObject.transform);

            // Установим параметры для UI-объекта
            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.SetParent(ContentObject.transform);
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;

            rectTransform.anchoredPosition3D = new Vector3(0f, PosY, 0f);
            PosY -= 45f;

            // Установка текста для элемента
            TextMeshProUGUI[] textComponents = obj.GetComponentsInChildren<TextMeshProUGUI>();
            textComponents[0].text = DBValues.Credit[i].ID.ToString();
            textComponents[1].text = DBValues.Credit[i].Money.ToString();

            Debug.Log($"{DBValues.Credit[i].ID}, {DBValues.Credit[i].Money}");
        }
    }
}
