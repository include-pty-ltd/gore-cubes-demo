using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Include.UnityScript
{
    public class UnityXRAvatarTracker : MonoBehaviour
    {
        public XRNode node;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.localPosition = InputTracking.GetLocalPosition(node);
            this.transform.localRotation = InputTracking.GetLocalRotation(node);
        }
    }
}