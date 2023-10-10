using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventFather 
{
    private static UnityEvent _light_red = new UnityEvent();
    private static UnityEvent _light_green = new UnityEvent();

    public static void SubscibeToRedLight(UnityAction action)
    {
        _light_red.AddListener(action);
    }

    public static void SubscribeToGreenLight(UnityAction action)
    {
        _light_green.AddListener(action);
    }

    public static void RedLight()
    {
        _light_red.Invoke();
    }

    public static void GreenLight()
    {
        _light_green.Invoke();
    }
}
