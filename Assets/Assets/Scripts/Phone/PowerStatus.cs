//using System.Windows.Forms;
//using UnityEngine;

//public class PowerStatus : MonoBehaviour
//{
//    void Start()
//    {
//        // �������� ���������� � ������ �������
//        PowerStatus powerStatus = SystemInformation.PowerStatus;
//        float batteryLifePercent = powerStatus.BatteryLifePercent * 100;

//        Debug.Log("����� �������: " + batteryLifePercent + "%");

//        // ��������, ���������� �� �������� ����������
//        if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
//        {
//            Debug.Log("������� ��������� � �������.");
//        }
//        else
//        {
//            Debug.Log("������� �� ��������� � �������.");
//        }
//    }
//}
