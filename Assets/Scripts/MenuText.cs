using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    
    //controls the name of the folio displayed in the control panel
    
    //takes in list of names from inspector
    [SerializeField] private List<string> stateList = new List<string>();
    private BookManager bookManager; 
    private int stateNum = 0;

    private void Start()
    {
        if (stateList.Count > 0)
        {
            GetComponent<TextMeshPro>().text = stateList[0];
        }
        bookManager = GameObject.Find("Book Manager").GetComponent<BookManager>();
    }

    public void IncrementState()
    {
        stateNum++;
        if (stateNum >= stateList.Count)
        {
            stateNum = 0;
        }
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }
    
    public void DecrementState()
    {
        stateNum--;
        if (stateNum < 0)
        {
            stateNum = stateList.Count-1;
        }
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }

    public void updateState() // increment and decrement don't always line up, so instead, we will directly take the page number from the bookManager itself
    {
        int currLeft = bookManager.leftPageNum;

        if (currLeft == 0) // r = 2
        {
            stateNum = 0; 
        }
        else if (currLeft == 1) // r = 0
        {
            stateNum = 1;
        }
        else if (currLeft == 2) // r = 1 
        {
            stateNum = 2; 
        }
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }

    public void ResetExperienceMT()
    {
        stateNum = 0;
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }
}
