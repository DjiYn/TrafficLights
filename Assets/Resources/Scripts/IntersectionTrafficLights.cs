using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class IntersectionTrafficLights : MonoBehaviour
{
    public TrafficLightSettings settings;

    private struct TrafficLight
    {
        public float time;
        public Renderer topLight;
        public Renderer middleLight;
        public Renderer bottomLight;
        public string color;
        public string colorText;
    }

    private TrafficLight[] trafficLights;

    private Material greenLightMaterial;
    private Material redLightMaterial;
    private Material yellowLightMaterial;
    private Material noLightMaterial;

    void Start()
    {
        InitiatializeTrafficLights();

        greenLightMaterial = Resources.Load<Material>("Materials/Green Light");
        redLightMaterial = Resources.Load<Material>("Materials/Red Light");
        yellowLightMaterial = Resources.Load<Material>("Materials/Amber Light");
        noLightMaterial = Resources.Load<Material>("Materials/No Light");

        ResetTrafficLights();
        SetTrafficlLightToUnmanaged();
    }

    void Update()
    {
        ManageTrafficLights();
    }

    private void UpdateTrafficLightsTime()
    {
        for (int i = 0; i < trafficLights.Length; i++)
        {
            trafficLights[i].time += Time.deltaTime;
        }
    }

    private void InitiatializeTrafficLights()
    {
        trafficLights = new TrafficLight[4];

        int i = 0;

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Traffic Light"))
            {
                trafficLights[i].time = 0;
                trafficLights[i].topLight = child.transform.Find("Top Light").GetComponent<Renderer>();
                trafficLights[i].middleLight = child.transform.Find("Middle Light").GetComponent<Renderer>();
                trafficLights[i].bottomLight = child.transform.Find("Bottom Light").GetComponent<Renderer>();
                
                i++;
            }
        }
    }

    private void ResetTrafficLights()
    {
        for (int i = 0; i < trafficLights.Length; i++)
        {
            trafficLights[i].topLight.material = noLightMaterial;
            trafficLights[i].middleLight.material = noLightMaterial;
            trafficLights[i].bottomLight.material = noLightMaterial;
        }
    }

    private void ManageTrafficLights()
    {
        UpdateTrafficLightsTime();

        if (trafficLights[0].time <= Mathf.Abs(settings.signalTime - settings.notificationBeforeSwitchTime))
        {
            SetTrafficLightScenario(0);
        } else if (trafficLights[0].time <= Mathf.Abs(settings.signalTime))
        {
            SetTrafficLightScenario(1);
        } else if (trafficLights[0].time <= Mathf.Abs(settings.signalTime + settings.signalTime - settings.notificationBeforeSwitchTime))
        {
            SetTrafficLightScenario(2);
        } else if (trafficLights[0].time <= Mathf.Abs(settings.signalTime + settings.signalTime))
        {
            SetTrafficLightScenario(3);
        }
        else
        {
            trafficLights[0].time = 0;
        }
    }

    private void SetTrafficLightScenario(int scenarioNumber)
    {
        switch (scenarioNumber)
        {
            case 0:
                SetTrafficLightToColor(0, "Red");
                SetTrafficLightToColor(2, "Red");

                SetTrafficLightToColor(1, "Green");
                SetTrafficLightToColor(3, "Green");
                break;

            case 1:
                SetTrafficLightToColor(0, "Red/Yellow");
                SetTrafficLightToColor(2, "Red/Yellow");

                SetTrafficLightToColor(1, "Yellow");
                SetTrafficLightToColor(3, "Yellow");
                break;

            case 2:
                SetTrafficLightToColor(0, "Green");
                SetTrafficLightToColor(2, "Green");

                SetTrafficLightToColor(1, "Red");
                SetTrafficLightToColor(3, "Red");
                break;

            case 3:
                SetTrafficLightToColor(0, "Yellow");
                SetTrafficLightToColor(2, "Yellow");

                SetTrafficLightToColor(1, "Red/Yellow");
                SetTrafficLightToColor(3, "Red/Yellow");
                break;

            default:
                SetTrafficlLightToUnmanaged();
            break;
        }
    }

    private void SetTrafficLightToColor(int index, string colorName)
    {
        trafficLights[index].color = colorName;

        if (colorName == "Red")
        {
            trafficLights[index].topLight.material = redLightMaterial;
            trafficLights[index].middleLight.material = noLightMaterial;
            trafficLights[index].bottomLight.material = noLightMaterial;
            trafficLights[index].colorText = settings.redLightText;
        }

        if (colorName == "Yellow")
        {
            trafficLights[index].topLight.material = noLightMaterial;
            trafficLights[index].middleLight.material = yellowLightMaterial;
            trafficLights[index].bottomLight.material = noLightMaterial;
            trafficLights[index].colorText = settings.yellowLightText;

        }

        if (colorName == "Green")
        {
            trafficLights[index].topLight.material = noLightMaterial;
            trafficLights[index].middleLight.material = noLightMaterial;
            trafficLights[index].bottomLight.material = greenLightMaterial;
            trafficLights[index].colorText = settings.greenLightText;
        }

        if (colorName == "Red/Yellow")
        {
            trafficLights[index].topLight.material = redLightMaterial;
            trafficLights[index].middleLight.material = yellowLightMaterial;
            trafficLights[index].bottomLight.material = noLightMaterial;
            trafficLights[index].colorText = settings.redYellowLightText;
        }
    }

    private void SetTrafficlLightToUnmanaged()
    {
        SetTrafficLightToColor(0, "Yellow");
        SetTrafficLightToColor(1, "Yellow");
        SetTrafficLightToColor(2, "Yellow");
        SetTrafficLightToColor(3, "Yellow");
    }

    public string GetTrafficLightColor(int index)
    {
        return trafficLights[index].color;
    }

    public string GetTrafficLightColorText(int index)
    {
        return trafficLights[index].colorText;
    }

    public int GetTrafficLightPoleIndex(Transform trafficLightPole)
    {
        for (int i = 0; i < trafficLights.Length; i++)
        {
            if (trafficLights[i].topLight == trafficLightPole.Find("Top Light").GetComponent<Renderer>())
            { 
                return i;
            }
        }

        return -1;
    }

    public bool IsAbleToDrive(int index)
    {
        switch (this.GetTrafficLightColor(index))
        {
            case "Red":
                return false;

            case "Yellow":
                return true;

            case "Green":
                return true;

            default:
                return false;
        }
    }
}
