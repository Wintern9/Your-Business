//using System.Windows.Forms;
//using UnityEngine;

//public class PowerStatus : MonoBehaviour
//{
//    void Start()
//    {
//        // Получаем информацию о заряде батареи
//        PowerStatus powerStatus = SystemInformation.PowerStatus;
//        float batteryLifePercent = powerStatus.BatteryLifePercent * 100;

//        Debug.Log("Заряд батареи: " + batteryLifePercent + "%");

//        // Проверка, подключено ли зарядное устройство
//        if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
//        {
//            Debug.Log("Ноутбук подключен к зарядке.");
//        }
//        else
//        {
//            Debug.Log("Ноутбук не подключен к зарядке.");
//        }
//    }
//}
