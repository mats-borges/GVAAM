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
    private Quaternion defaultRotation;
    private Quaternion defaultSimRotation;
    [SerializeField] private GameObject _spineCenter;
    [SerializeField] private GameObject _gp;
    private float speed = 1f;
    private float t;

    private ObiParticleGroup particleGroup;
    private ObiActor actor;
    private int particleGroupIndex;
    [SerializeField] private string particleGroupName;
    [SerializeField] private int particleIdx = -1;
    private bool isGripping = false;

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
        defaultRotation = mesh.transform.rotation;
        defaultSimRotation = Quaternion.Euler(0.2f, 0f, 0f);
        t = 0f;
        isGripping = false;
    }

    public void handStatus()
    {
        isGripping = !isGripping;
    }

    // Update is called once per frame
    private void Update() 
    {
        GameObject currPage = _graspingPoint.GetSimPageSide() == side ?  simPage : sidePage; // see which side the sim page is on 
        Vector3 loc = actor.GetParticlePosition(particleIdx);

        if (currPage == simPage)
        {
            if (isGripping)
            {
                Vector3 lineVector = _gp.transform.position - _spineCenter.transform.position;
                lineVector.z = 0;
                float angle = Vector3.Angle(lineVector, Vector3.left);
                Quaternion rotationQuaternion = Quaternion.Euler(0.2f, 0f, -1 * angle);
                Vector3 perpendicularVector = Vector3.Cross(lineVector, Vector3.forward);
                perpendicularVector.Normalize();
                mesh.transform.SetPositionAndRotation(loc + perpendicularVector * .7f, rotationQuaternion);
            }
            else if (mesh.transform.rotation != defaultSimRotation)
            {
                Vector3 lineVector = _gp.transform.position - _spineCenter.transform.position;
                lineVector.z = 0;
                float angle = Vector3.Angle(lineVector, Vector3.left);
                Quaternion rotationQuaternion = Quaternion.Lerp(Quaternion.Euler(0.2f, 0f, -1 * angle), defaultSimRotation, Mathf.Min(t*speed, 1f));
                Vector3 perpendicularVector = Vector3.Cross(lineVector, Vector3.forward);
                perpendicularVector.Normalize();
                mesh.transform.SetPositionAndRotation(loc + perpendicularVector * .7f, rotationQuaternion);
                t += Time.deltaTime;
            }
            else
            {
                t = 0f;
            }
            
            // Move the sphere to the calculated closest point
            Vector3 closestPointOnTarget = sidePage.GetComponent<Collider>().ClosestPoint(loc);
            staticLoc = closestPointOnTarget + new Vector3(0.1f, -0.3f, 0.0f);
        }
        else
        {
            mesh.transform.position = staticLoc;
            mesh.transform.rotation = defaultRotation; 
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

