using System.Collections;
using System.Collections.Generic;
using System.Data;
using HandPhysicsToolkit.Helpers.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowObject : MonoBehaviour
{
    private Transform targetObject;
    private bool _isHandTracking = false;
    //private bool _isLeftHand = false;
    private BaseInteractor currentInteractor = null;
    private bool grabbing = false;
    //private bool wasGrabbing = false;

    [Header("Controller Offset")]
    [FormerlySerializedAs("pinchPosOffset")] [SerializeField] private Vector3 controllerPosOffset;
    [FormerlySerializedAs("pinchRotOffset")] [SerializeField] private Vector3 controllerRotOffset;
    
    
    [Header("Handtracking Offset")]
    [FormerlySerializedAs("gripPosOffset")] [SerializeField] private Vector3 handtrackingPosOffset;
    [FormerlySerializedAs("gripRotOffset")] [SerializeField] private Vector3 handtrackingRotOffset;

    public int layerMask; // The layer(s) the table belongs to
    public float springForce = 10f; // The force of the spring

    private Rigidbody rigidbody;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        layerMask = 1 << LayerMask.NameToLayer("Default");

    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject)
        {

            // Calculate the direction from the ruler to the hand
            Vector3 directionToHand = targetObject.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToHand.normalized, out hit, directionToHand.magnitude, layerMask))
            {
                // Calculate the distance from the hit point to the ruler
                float distanceToHit = Vector3.Distance(transform.position, hit.point);

                // Apply a spring force to move the ruler away from the table
                rigidbody.AddForce(directionToHand.normalized * (springForce / distanceToHit), ForceMode.Force);
            }
            else
            {
                Vector3 newPos = targetObject.transform.position;
                _isHandTracking = currentInteractor.GetIsHandTracking();


                var posOffset = (!_isHandTracking) ? controllerPosOffset : handtrackingPosOffset;
                var rotOffset = (!_isHandTracking) ? controllerRotOffset : handtrackingRotOffset;

                // Define a speed for your interpolation
                float moveSpeed = .5f; // You can adjust this as needed
                float rotationSpeed = .5f; // You can adjust this as needed
                float positionChangeMagnitude = Vector3.Distance(transform.position, newPos);
                float rotationChangeMagnitude = Quaternion.Angle(transform.rotation, targetObject.rotation);


                Vector3 smoothedPos = Vector3.Lerp(transform.position, newPos, moveSpeed);
                Quaternion smoothedRot = Quaternion.Slerp(transform.rotation, targetObject.rotation * Quaternion.Euler(0, 180f, 0) * Quaternion.Euler(rotOffset), rotationSpeed);

                transform.position = smoothedPos + posOffset;
                transform.rotation = smoothedRot; //  * Quaternion.Euler(rotOffset);


                // transform.Rotate(rotOffset);


                if (positionChangeMagnitude >= 3f)
                {
                    // Lerp the position
                    //transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed);
                    // Apply the position offset
                    //transform.position += posOffset;
                    transform.position = smoothedPos + posOffset;
                }

                if (rotationChangeMagnitude >= 50f)
                {
                    transform.rotation = smoothedRot;
                    // Slerp the rotation
                    /*
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetObject.rotation, rotationSpeed);

                    if (!_isLeftHand && _isHandTracking)
                    {
                        rotOffset.x -= 180f;
                        rotOffset.z -= 180f;
                    }
                    // Rotate by the offset
                    transform.Rotate(rotOffset);
                    */
                }
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the colliders intersect while holding the object
        //if ((collision != null) && grabbing == true)
        //{
        //    // Respond to the collision
        //    // For instance, you can adjust the position of one of the colliders to prevent intersection
        //    // Example: Move the object slightly away from the collision point
        //    // Get the collision normal vector
        //    // Vector3 collisionNormal = collision.contacts[0].normal;

        //    //collisionNormal.Normalize(); // normalize between 0 and 1

        //    transform.position += collision.contacts[0].normal * 20f;
        //}

        if (targetObject == null && collision != null)
        {
            transform.position += Vector3.up * 10f;
        }
    }

    public void RegisterTarget(BaseInteractor interactor)
    {
        if (targetObject != null) return;

        targetObject = interactor.GetGameObject().transform;
        grabbing = true;
        //wasGrabbing = false;
        // Attach the magnifying glass to the hand and adjust position
        //transform.SetParent(interactor.GetGameObject().transform);
        currentInteractor = interactor;

        //_isLeftHand = interactor.GetIsLeftHand();
        //attach();
    }
    
    public void UnregisterTarget()
    {
        if (targetObject == null) return;

        grabbing = false;
        //wasGrabbing = true;
        // Release the magnifying glass to the hand and adjust position
        //transform.SetParent(null);


        targetObject = null;
        currentInteractor = null;
        //dettach();
    }
}
