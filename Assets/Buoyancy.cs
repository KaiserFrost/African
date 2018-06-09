using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

    private Vector3 upLift, actionPoint;
    public Vector3 buoyancyoffset;
    public float floatHeight, bounceDamp;
    private float forceFactor;
    public float waterlevel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        actionPoint = transform.position + transform.TransformDirection(buoyancyoffset);
        forceFactor = 1f - ((actionPoint.y - waterlevel) / floatHeight);
        if (forceFactor > 0f)
        {
            upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
        }

    }
}
