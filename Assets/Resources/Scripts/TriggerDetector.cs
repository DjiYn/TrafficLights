using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerDetector : MonoBehaviour
{
    public DriveTextManager driveTextManager;


    private bool isInsidePlayerTrigger = false;
    private IntersectionTrigger intersectionTrigger;


    private void Start()
    {
        intersectionTrigger = transform.GetComponent<IntersectionTrigger>();
    }

    private void Update()
    {
        if (isInsidePlayerTrigger)
        {
            driveTextManager.SetIsAbleToDrive(intersectionTrigger.GetIsAbleToDrive());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsidePlayerTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsidePlayerTrigger = false;
        }
    }
}
