using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour {

    public Hand hand;
    public Transform finger1;
    public Transform finger2;

    XRNode node;
    string axis;
    Transform t;
    bool open = true;

	// Use this for initialization
	void Start () {
        t = transform;
        switch (hand)
        {
            case Hand.Left:
                node = XRNode.LeftHand;
                axis = "Left Trigger";
                break;
            case Hand.Right:
                node = XRNode.RightHand;
                axis = "Right Trigger";
                break;
        }

	}
	
	// Update is called once per frame
	void Update () {
        t.position = InputTracking.GetLocalPosition(node);
        t.rotation = InputTracking.GetLocalRotation(node);
        
        if ((Input.GetAxis(axis) > 0.5f) && open)
        {
            open = false;
            finger1.localPosition = new Vector3(0.25f, 0, 0);
            finger2.localPosition = new Vector3(-0.25f, 0, 0);

        }
        if (!(Input.GetAxis(axis) > 0.5f) && !open)
        {
            open = true;
            finger1.localPosition = new Vector3(0.5f, 0, 0);
            finger2.localPosition = new Vector3(-0.5f, 0, 0);
        }
	}
}

public enum Hand
{
    Left, Right
}