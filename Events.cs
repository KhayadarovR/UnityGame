using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Events : MonoBehaviour
{

    public static event Action<bool> Air; // 

    public static void OnAir(bool value)
    {
        Air?.Invoke(value);
        //Debug.Log("���������� - ������ �� � �����");
    }

    //TO DO: OnDie, OnChangeHealth
}
