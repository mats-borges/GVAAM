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
    private bool _isLeftHand = false;
    private BaseInteractor currentInteractor = null;
    
    [Header("Controller Offset")]
    [FormerlySerializedAs("pinchPosOffset")] [SerializeField] private Vector3 controllerPosOffset;
    [FormerlySerializedAs("pinchRotOffset")] [SerializeField] private Vector3 controllerRotOffset;
    
    
    [Header("Handtracking Offset")]
    [FormerlySerializedAs("gripPosOffset")] [SerializeField] private Vector3 handtrackingPosOffset;
    [FormerlySerializedAs("gripRotOffset")] [SerializeField] private Vector3 handtrackingRotOffset;
    

    // Update is called once per frame
    void Update()
    {
        if (targetObject)
        {
            Vector3 newPos = targetObject.transform.position;
            _isHandTracking = currentInteractor.GetIsHandTracking();

            var posOffset = (!_isHandTracking) ? controllerPosOffset : handtrackingPosOffset;
            var rotOffset = (!_isHandTracking) ? controllerRotOffset : handtrackingRotOffset;

            // Define a speed for your interpolation
            float moveSpeed = 100000f; // You can adjust this as needed
            float rotationSpeed = 360; // You can adjust this as needed
            float positionChangeMagnitude = Vector3.Distance(transform.position, newPos);
            float rotationChangeMagnitude = Quaternion.Angle(transform.rotation, targetObject.rotation);

            if (positionChangeMagnitude >= 3f)
            {
                // Lerp the position
                transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed);
                // Apply the position offset
                transform.position += posOffset;
            }

            if (rotationChangeMagnitude >= 10)
            {
                // Slerp the rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetObject.rotation, rotationSpeed);

                if (!_isLeftHand && _isHandTracking)
                {
                    rotOffset.x -= 180f;
                    rotOffset.z -= 180f;
                }
                // Rotate by the offset
                transform.Rotate(rotOffset);
            }
        }
    }

    public void RegisterTarget(BaseInteractor interactor)
    {
        if (targetObject != null) return;

        targetObject = interactor.GetGameObject().transform;
        currentInteractor = interactor;

        _isLeftHand = interactor.GetIsLeftHand();
    }
    
    public void UnregisterTarget()
    {
        if (targetObject == null) return;
        
        targetObject = null;
        currentInteractor = null;
    }
}
