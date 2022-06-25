using UnityEngine;
using System;

public class Events : MonoBehaviour
{
    //��� ���������� ����������!
    public static event Action<bool> Air; // 

    public static void OnAir(bool value)
    {
        Air?.Invoke(value);
        //Debug.Log("���������� - ������ �� � �����");
    }

    public static event Action<float> HealthChanged; //�������� ������ �� ��������

    public static void OnHealthChanged(float value) //���������� ���� �� ��������� � - ������
    {
        HealthChanged?.Invoke(value);
    }

    public static event Action MoneyChange; // 

    public static void OnMoneyChange()
    {
        MoneyChange?.Invoke();
    }
}
