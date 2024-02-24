using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBadge : MonoBehaviour
{
    [SerializeField] GameObject badgeInteractive;
    bool isOn = false;

    private void Start()
    {
        badgeInteractive.SetActive(false);
        isOn = false;
    }

    public void toggleBadgeInteractive()
    {
        if (isOn)
        {
            badgeInteractive.SetActive(false);
            isOn = false;
        }
        else
        {
            badgeInteractive.SetActive(true);
            isOn = true;
        }
    }
}
