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
        float PosY = -150f;
        Debug.Log(DBValues.Credit.Count);
        for (int i = 0; i < DBValues.Credit.Count; i++) {
            Debug.Log(DBValues.Credit[i]);
            GameObject obj = Instantiate(item);
            obj.transform.SetParent(ContentObject.transform);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = new Vector3(0f, PosY, 0f);
            PosY -= 45f;

            TextMeshProUGUI[] textComponents = ContentObject.GetComponentsInChildren<TextMeshProUGUI>();
            textComponents[0].text = DBValues.Credit[i].ID.ToString();
            textComponents[1].text = DBValues.Credit[i].Money.ToString();
        }
    }
}
