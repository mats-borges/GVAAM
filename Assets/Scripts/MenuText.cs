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
    private PagesideTextManager pagesideTextManager; 
    private int stateNum = 0;

    private void Start()
    {
        if (stateList.Count >= 0)
        {
            GetComponent<TextMeshPro>().text = stateList[0]; // always start at the first page
        }
        pagesideTextManager = GameObject.Find("Pageside Text Manager").GetComponent<PagesideTextManager>();
    }

    // update based on code in PagesideTextManager that recognizes if the page has been turned
    public void updateState() 
    {
        stateNum = pagesideTextManager.getPageNum()/2; // get the left page number
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }

    public void ResetExperienceMT()
    {
        stateNum = 0;
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }

    /* old code
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
            stateNum = stateList.Count - 1;
        }
        GetComponent<TextMeshPro>().text = stateList[stateNum];
    }
    */ 
}
