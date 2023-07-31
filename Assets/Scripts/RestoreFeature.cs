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
    [SerializeField] GameObject mesh;

    [SerializeField] Material restoredMaterial;
    private bool isRestored;
    private Vector3 staticLoc;

    private Material originalMat;
    [SerializeField] private Material highlightMat;
    public float fadeDuration = 1.0f;


    // Start is called before the first frame update
    private void Awake()
    {
        actor = simPage.GetComponent<ObiActor>();
        mesh.layer = LayerMask.NameToLayer("OutlineLayer");
        side = onLeft ? GraspingPoint.SimPageSide.LeftSide : GraspingPoint.SimPageSide.RightSide;
        isRestored = false;
        originalMat = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    private void Update() 
    {
        GameObject currPage = _graspingPoint.GetSimPageSide() == side ?  simPage : sidePage; // see which side the sim page is on 
        Vector3 loc = actor.GetParticlePosition(particleIdx);

        if (currPage == simPage)
        {
            mesh.transform.SetPositionAndRotation(loc, Quaternion.identity);
            Vector3 closestPointOnTarget = sidePage.GetComponent<Collider>().ClosestPointOnBounds(loc);

            // Move the sphere to the calculated closest point
            staticLoc = closestPointOnTarget + new Vector3(0.1f, -.3f, 0.0f);
        }
        else
        {
            mesh.transform.position = staticLoc;
        }
        
    }

    public void toggleRestored()
    {
        if (!isRestored)
        {
            isRestored = true;
            StartCoroutine(fadeOut(originalMat, restoredMaterial)); // fade effect
            mesh.GetComponent<MeshRenderer>().material = restoredMaterial;
            mesh.GetComponent<MaterialSwitcher>().setHighlightMat(restoredMaterial);
            mesh.GetComponent<MaterialSwitcher>().setOriginalMat(restoredMaterial);
        }
        else
        {
            isRestored = false;
            StartCoroutine(fadeOut(restoredMaterial, originalMat));
            mesh.GetComponent<MeshRenderer>().material = originalMat;
            mesh.GetComponent<MaterialSwitcher>().setHighlightMat(highlightMat);
            mesh.GetComponent<MaterialSwitcher>().setOriginalMat(originalMat);
        }
    }

    public IEnumerator fadeOut(Material startMat, Material endMat)
    {
        float elapsedTime = 0f;
        Material newMaterial = new Material(startMat);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            // interpolate the material properties 
            newMaterial.color = Color.Lerp(startMat.color, endMat.color, t);
            newMaterial.mainTextureOffset = Vector2.Lerp(startMat.mainTextureOffset, endMat.mainTextureOffset, t);

            mesh.GetComponent<MeshRenderer>().material = newMaterial;

            yield return null;
        }

        mesh.GetComponent<MeshRenderer>().material = endMat;
    }

}

