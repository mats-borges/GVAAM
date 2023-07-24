using System.Collections;
using System.Collections.Generic;
using HandPhysicsToolkit.Helpers.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Obi;
using UnityFx.Outline;

public class RestoreFeature : MonoBehaviour
{
    [SerializeField] private GameObject simPage; // simpage
    [SerializeField] private GameObject sidePage; // non-simpage side page 
    [SerializeField] private Material triggerMat;
    [SerializeField] private bool onLeft;

    private ObiParticleGroup particleGroup;
    private ObiActor actor;
    private int particleGroupIndex;
    [SerializeField] private string particleGroupName;
    [SerializeField] private int particleIdx = -1;

    [SerializeField] private GraspingPoint _graspingPoint;
    private GraspingPoint.SimPageSide side;
    [SerializeField] GameObject staticVersion;

    [SerializeField] Material restoredMaterial;
    private bool isRestored; 
    

    // Start is called before the first frame update
    private void Awake()
    {
        actor = simPage.GetComponent<ObiActor>();

        gameObject.layer = LayerMask.NameToLayer("Default");
        staticVersion.SetActive(false);
        staticVersion.layer = LayerMask.NameToLayer("Default");

        side = onLeft ? GraspingPoint.SimPageSide.LeftSide : GraspingPoint.SimPageSide.RightSide;

        isRestored = false;
    }

    // Update is called once per frame
    private void Update() // TO-DO: figure out why it isn't working to hide when become circle
    {
        GameObject currPage = _graspingPoint.GetSimPageSide() == side ?  simPage : sidePage; // see which side the sim page is on 
        transform.SetPositionAndRotation(actor.GetParticlePosition(particleIdx), Quaternion.identity);

        if (currPage == simPage)
        {
            gameObject.layer = LayerMask.NameToLayer("OutlineLayer");
            staticVersion.SetActive(false);
            staticVersion.layer = LayerMask.NameToLayer("Default");

            Vector3 particlePosition = actor.GetParticlePosition(particleIdx); // returns in world space 

            // Calculate the closest point on the target GameObject to the particle's position
            Vector3 closestPointOnTarget = sidePage.GetComponent<Collider>().ClosestPointOnBounds(particlePosition);

            // Move the sphere to the calculated closest point
            staticVersion.transform.position = closestPointOnTarget + new Vector3(0.0f, -.5f, 0.0f);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            staticVersion.SetActive(true);
            staticVersion.layer = LayerMask.NameToLayer("OutlineLayer");
        }
        
    }

    public void displayRestored()
    {
        gameObject.GetComponent<MeshRenderer>().material = restoredMaterial;
        staticVersion.GetComponent<MeshRenderer>().material = restoredMaterial;

        isRestored = true;

        gameObject.GetComponent<MaterialSwitcher>().setHighlightMat(restoredMaterial);
        gameObject.GetComponent<MaterialSwitcher>().setOriginalMat(restoredMaterial);
        staticVersion.GetComponent<MaterialSwitcher>().setHighlightMat(restoredMaterial);
        staticVersion.GetComponent<MaterialSwitcher>().setOriginalMat(restoredMaterial);
    }

}

