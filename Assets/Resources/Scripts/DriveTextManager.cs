using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DriveTextManager : MonoBehaviour
{
    public TextMeshProUGUI driveText;

    private bool isAbleToDrive = false;


    public void ShowDriveText()
    {
        if (isAbleToDrive)
        {
            driveText.text = "Can drive";
        } else
        {
            driveText.text = "Can't drive";
        }
    }

    public void SetIsAbleToDrive(bool isAbleToDrive)
    {
        this.isAbleToDrive = isAbleToDrive;
    }

}
