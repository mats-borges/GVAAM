using System.Collections;
using System.Collections.Generic;
using HandPhysicsToolkit.Helpers.Interfaces;
using Obi;
using UnityEngine;

public class BookManager : MonoBehaviour
{

    // functions for updating the pages shown in the book and the corresponding pages of the inspector

    //leftpagenum is used by pagesideTextManager for the default page
    public int leftPageNum = 0; // change this to private later
    int simPageNum = 1; // simpage is initially right page 
    public int rightPageNum = 2; // was 2
    int versoInspectorPageNum = 0;
    int rectoInspectorPageNum = 1;
    public bool pairedMode = true;
    private bool isInspectorOn = true;

    [SerializeField] GameObject rightPage;
    [SerializeField] GameObject leftPage;
    [SerializeField] GameObject simPage;
    [SerializeField] GameObject versoInspectorPage;
    [SerializeField] GameObject rectoInspectorPage;
    [SerializeField] private GameObject bookSystemObject;

    [SerializeField] List<Material> pageList = new List<Material>(); // stores the page materials
    [SerializeField] List<Material> restoredList = new List<Material>(); // stores the restored pages
    private List<Material> currList; // used to refernce pageList or restoredList
    private bool restoreMode = false; 

    public int getLeftPageNum()
    {
        return leftPageNum;
    }

    // Sets defaults 
    private void Start()
    {
        currList = pageList;
        restoreMode = false;
        rightPage.GetComponent<Renderer>().material = currList[rightPageNum];
        leftPage.GetComponent<Renderer>().material = currList[leftPageNum];
        simPage.GetComponent<Renderer>().material = currList[simPageNum];
        versoInspectorPage.GetComponent<Renderer>().material = currList[versoInspectorPageNum];
        rectoInspectorPage.GetComponent<Renderer>().material = currList[rectoInspectorPageNum];
        //folioMenuText = GameObject.Find("Advance Page Name Text");
    }

    public void ToggleRestore()
    {
        if (restoreMode) // turn off restored 
        {
            currList = pageList;
            restoreMode = false; 
        }
        else // turn on restored 
        {
            currList = restoredList;
            restoreMode = true; 
        }
        // update all renders
        rightPage.GetComponent<Renderer>().material = currList[rightPageNum];
        leftPage.GetComponent<Renderer>().material = currList[leftPageNum];
        simPage.GetComponent<Renderer>().material = currList[simPageNum];
        versoInspectorPage.GetComponent<Renderer>().material = currList[versoInspectorPageNum];
        rectoInspectorPage.GetComponent<Renderer>().material = currList[rectoInspectorPageNum];
    }

    public void Increment(GameObject page,ref int pgn)
    {
        pgn++;
        if(pgn>= currList.Count)
        {
            pgn = 0;
        }
        page.GetComponent<Renderer>().material = currList[pgn];
    }

    public void Decrement(GameObject page,ref int pgn)
    {
        pgn--;
        if ( pgn < 0)
        {
            pgn = currList.Count-1;
        }
        page.GetComponent<Renderer>().material = currList[pgn];
    }

    public void IncrementAll()
    {
        Increment(rightPage, ref rightPageNum);
        Increment(leftPage, ref leftPageNum);
        Increment(simPage, ref simPageNum);
        if (pairedMode)
        {
            Increment(versoInspectorPage,  ref versoInspectorPageNum);
            Increment(rectoInspectorPage,  ref rectoInspectorPageNum);
        }

    }

    public void decrementAll()
    {
        Decrement(rightPage,ref rightPageNum);
        Decrement(leftPage,ref leftPageNum);
        Decrement(simPage,ref simPageNum);
        if (pairedMode)
        {
            Decrement(versoInspectorPage,  ref versoInspectorPageNum);
            Decrement(rectoInspectorPage,  ref rectoInspectorPageNum);
        }

    }

    //increments only the inspector
    public void IncrementInspector()
    {
        Increment(versoInspectorPage, ref versoInspectorPageNum);
        Increment(rectoInspectorPage, ref rectoInspectorPageNum);
    }
    
    //decrements only the inspector
    public void DecrementInspector()
    {
        Decrement(versoInspectorPage, ref versoInspectorPageNum);
        Decrement(rectoInspectorPage, ref rectoInspectorPageNum);
    }

    //sets inspector pages to book's currently displayed pages, then inverts attachedMode bool
    public void ToggleInspectorMode()
    {
        if (pairedMode == false)
        {
            versoInspectorPageNum = leftPageNum;
            rectoInspectorPageNum = simPageNum;
            versoInspectorPage.GetComponent<Renderer>().material = currList[versoInspectorPageNum];
            rectoInspectorPage.GetComponent<Renderer>().material = currList[rectoInspectorPageNum];
        }
        pairedMode = !pairedMode;
    }

    public void ToggleInspector()
    {
        isInspectorOn = !isInspectorOn;
        versoInspectorPage.GetComponent<MeshRenderer>().enabled = isInspectorOn;
        rectoInspectorPage.GetComponent<MeshRenderer>().enabled = isInspectorOn;
    }

    public void ResetExperienceBM()
    {
        currList = pageList;
        restoreMode = false;
        leftPageNum = 0;
        simPageNum = 1;
        rightPageNum = 2; 
        versoInspectorPageNum = 0;
        rectoInspectorPageNum = 1;
        leftPage.GetComponent<Renderer>().material = currList[leftPageNum];
        simPage.GetComponent<Renderer>().material = currList[simPageNum];
        rightPage.GetComponent<Renderer>().material = currList[rightPageNum];
        versoInspectorPage.GetComponent<Renderer>().material = currList[versoInspectorPageNum];
        rectoInspectorPage.GetComponent<Renderer>().material = currList[rectoInspectorPageNum];

        ResetPageParticles(null);
        bookSystemObject.GetComponent<ParticlePositionManager>().LoadParticles("RightSideResting");
    }

    IEnumerator ResetPageRoutine(BaseInteractor interactor)
    {
        simPage.GetComponent<ObiTearableCloth>().ResetParticles();
        simPage.GetComponent<ObiTearableCloth>().RemoveFromSolver();
        simPage.GetComponent<ObiTearableCloth>().AddToSolver();

        yield return new WaitForEndOfFrame();

        yield return null;
    }

    public void ResetPageParticles(BaseInteractor interactor)
    {
        StartCoroutine(ResetPageRoutine(interactor));
    }
    
    
}
