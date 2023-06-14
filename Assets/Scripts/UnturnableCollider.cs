using System.Collections;
using System.Collections.Generic;
using HandPhysicsToolkit.Helpers.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class UnturnableCollider : MonoBehaviour
{
    //controls the page turning "magic trick" 
    
    //check if player is trying to grab
    //check if grabber is on the side closer to the hand
    //invoke the magic trick unity events in the inspector
    
    public GraspingPoint.SimPageSide colliderSide = GraspingPoint.SimPageSide.RightSide;

    //InteractibleEvent is defined in Interactible.cs
    [SerializeField] private InteractibleEvent pageMagic;
    [SerializeField] private GraspingPoint _graspingPoint;
    private bool isStart = true; 

    // if the grasping point is on the same side as the collider, do nothing
    // if the collider is not on the same side, it invokes the interactor

    public void PageMagicCheck(BaseInteractor interactor)
    {
        if (!_graspingPoint) return;

        var currentPageSide = _graspingPoint.GetSimPageSide();

        if (currentPageSide == colliderSide) return;

        if (isStart && currentPageSide != GraspingPoint.SimPageSide.RightSide) isStart = false; // no longer starting side     
        else if (!isStart) pageMagic.Invoke(interactor); // the incrementation of the page 
    }
}
