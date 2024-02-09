using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionReset : MonoBehaviour
{
    private Vector3 initPos;
    private Quaternion initRot;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPosition()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }
}
