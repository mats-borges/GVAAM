using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    private bool hidden;
    [SerializeField] private int index;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] MenuStatus mss;
    private MenuStatus ms; 

    // Start is called before the first frame update
    void Start()
    {
        hidden = true;
        ms = menuPanel.GetComponent<MenuStatus>();
    }


    public void activatePanel()
    {
        //ms.updateMenuDisplay(index);
        //menu.SetActive(true);
        //hidden = false;
    }

    
    public void onAndOff()
    {
        /*
        if (hidden == true)
        {
            StartCoroutine(OnDelay());
        }

        if (hidden == false)
        {
            StartCoroutine(OffDelay());
        }
        */
        /*
        if (hidden == false)
        {
            StartCoroutine(OffDelay());
        }
        */
    }

    
}
