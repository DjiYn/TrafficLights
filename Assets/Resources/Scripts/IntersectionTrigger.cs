using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class IntersectionTrigger : MonoBehaviour
{
    public TextMeshProUGUI trafficLightText;
    public GameObject trafficLightPole;
    public IntersectionTrafficLights trafficLightsIntersection;


    private int trafficLightIndex = -1;
    private bool isAbleToDrive = false;

    private void Update()
    {
        if (trafficLightIndex != -1)
        {
            trafficLightText.text = trafficLightsIntersection.GetTrafficLightColorText(trafficLightIndex);
            isAbleToDrive = trafficLightsIntersection.IsAbleToDrive(trafficLightIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trafficLightIndex = trafficLightsIntersection.GetTrafficLightPoleIndex(trafficLightPole.transform);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        trafficLightIndex = -1;
        trafficLightText.text = "";
        isAbleToDrive = false;
    }

    public bool GetIsAbleToDrive()
    {
        return isAbleToDrive;
    }
}
