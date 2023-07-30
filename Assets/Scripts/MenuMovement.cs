using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    public GameObject eyeTracker;

    private bool on;

    public void TrackEye()
    {
        transform.position = new Vector3(eyeTracker.transform.position.x, eyeTracker.transform.position.y, eyeTracker.transform.position.z);
        transform.eulerAngles = new Vector3(eyeTracker.transform.eulerAngles.x, eyeTracker.transform.eulerAngles.y, eyeTracker.transform.eulerAngles.z);
    }
}
