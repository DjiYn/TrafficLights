using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrafficLightSettings", menuName = "ScriptableObject/TrafficLightSettings")]
public class TrafficLightSettings : ScriptableObject
{
    [Min(2)]
    [Tooltip("Time in seconds, before red/green signal change.")]
    public int signalTime = 10;

    [Min(1)]
    [Tooltip("Time in seconds for yellow signal notification.")]
    public int notificationBeforeSwitchTime = 3;

    public string redLightText;
    public string yellowLightText;
    public string greenLightText;
    public string redYellowLightText;
}
