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
    public DriveTextManager driveTextManager;

    private int trafficLightIndex = -1;
    private bool isAbleToDrive = false;
    private bool isInsidePlayerTrigger = false;

    private void Update()
    {
        if (trafficLightIndex != -1 && isInsidePlayerTrigger)
        {
            trafficLightText.text = trafficLightsIntersection.GetTrafficLightColorText(trafficLightIndex);
            isAbleToDrive = trafficLightsIntersection.IsAbleToDrive(trafficLightIndex);
            driveTextManager.SetIsAbleToDrive(isAbleToDrive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trafficLightIndex = trafficLightsIntersection.GetTrafficLightPoleIndex(trafficLightPole.transform);
            isInsidePlayerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        trafficLightText.text = "";

        if (other.CompareTag("Player"))
        {
            isInsidePlayerTrigger = false;
            driveTextManager.SetIsAbleToDrive(true);
        }
    }
}
