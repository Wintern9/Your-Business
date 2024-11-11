using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyRoom : MonoBehaviour
{
    public Button ButtonRoom;
    public Button ButtonLevel;
    public Rooms Room;
    public TextMeshProUGUI text;

    public int ButtonInt;

    public int MoneyPay;
    public int LevelMoneyPay;

    public int RoomState = 0;

    public GameObject BuyObject;

    void Start()
    {
        ButtonRoom.onClick.AddListener(BuyR);
        ButtonLevel.onClick.AddListener(LevelUp);
    }

    void Update()
    {
        text.text = $"{DBValues.Places[ButtonInt]}/{DBValues.CountPlaces[ButtonInt]}";
    }

    void BuyR()
    {
        if(RoomState == 0)
        {
            BuyObject.SetActive(true);
            DBValues.Room++;

            DBValues.Player.Money -= MoneyPay;
            DBValues.Player.Save();
            RoomState = 1;
        }
    }

    [SerializeField] Button buttonLevelUpYes;
    
    void LevelUp()
    {
        buttonLevelUpYes.onClick.RemoveAllListeners();
        buttonLevelUpYes.onClick.AddListener(() => LevelUpYes(ButtonInt));
    }

    void LevelUpYes(int ButtonInt)
    {
        DBValues.CountPlaces[ButtonInt]++;
        DBValues.Player.Money -= LevelMoneyPay;
        DBValues.Player.Save();
    }
}
