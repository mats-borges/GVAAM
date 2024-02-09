using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStatus : MonoBehaviour
{
    //private GameObject currentDisplay;
    private GameObject lastDisplay;
    private bool on = true;
    private Dictionary<int, GameObject> displayMap;
    [SerializeField] private GameObject displayBook;
    [SerializeField] private GameObject displayAudio;
    [SerializeField] private GameObject displayControls;
    [SerializeField] private GameObject displayCredits;

    public void updateMenuDisplay(int display) // called by panels when activated
    {
        //currentDisplay = display;
    }


    // Start is called before the first frame update
    void Start()
    {
        displayMap = new Dictionary<int, GameObject>();
        displayMap.Add(1, displayBook);
        displayMap.Add(2, displayAudio);
        displayMap.Add(3, displayControls);
        displayMap.Add(4, displayCredits);

        //currentDisplay = displayAudio;
        lastDisplay = displayAudio;
        on = true;
    }

    public void bookPanelOnOff()
    {
        if (lastDisplay == displayBook && on == true)
        {
            displayBook.SetActive(false);
            on = false;
        }
        else if (lastDisplay != displayBook)
        {
            lastDisplay.SetActive(false);
            displayBook.SetActive(true);
            on = true;
        }

        lastDisplay = displayBook;
    }

    public void audioPanelOnOff()
    {
        if (lastDisplay == displayAudio && on == true)
        {
            displayAudio.SetActive(false);
            on = false;
        }
        else if (lastDisplay != displayAudio)
        {
            lastDisplay.SetActive(false);
            displayAudio.SetActive(true);
            on = true;
        }

        lastDisplay = displayAudio;
    }

    public void controlsPanelOnOff()
    {
        if (lastDisplay == displayControls && on == true)
        {
            displayControls.SetActive(false);
            on = false;
        }
        else if (lastDisplay != displayControls)
        {
            lastDisplay.SetActive(false);
            displayControls.SetActive(true);
            on = true;
        }

        lastDisplay = displayControls;
    }

    public void creditsPanelOnOff()
    { 
        if (lastDisplay == displayCredits && on == true) { 
            displayCredits.SetActive(false);
            on = false;
        }
        else if (lastDisplay != displayCredits) {
            lastDisplay.SetActive(false);
            displayCredits.SetActive(true);
            on = true;
        }

        lastDisplay = displayCredits;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (currentDisplay != lastDisplay) // new display
        {
            displayMap[lastDisplay].SetActive(false);
            displayMap[currentDisplay].SetActive(true);
            if (currentDisplay == 1)
            {
                displayBook.SetActive(true);
                displayAudio.SetActive(false);
                displayControls.SetActive(false);
                displayCredits.SetActive(false);
            }
            else if (currentDisplay == 2)
            {
                displayBook.SetActive(false);
                displayAudio.SetActive(true);
                displayControls.SetActive(false);
                displayCredits.SetActive(false);
            }
            else if (currentDisplay == 3)
            {
                displayBook.SetActive(false);
                displayAudio.SetActive(false);
                displayControls.SetActive(true);
                displayCredits.SetActive(false);
            }
            else if (currentDisplay == 4)
            {
                displayBook.SetActive(false);
                displayAudio.SetActive(false);
                displayControls.SetActive(false);
                displayCredits.SetActive(true);
            }

            currentDisplay = lastDisplay;
        }
        */

    }
}
