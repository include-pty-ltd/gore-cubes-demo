using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace Include.UnityScript
{
    class UnityXRTracker : MonoBehaviour
    {
        Transform t;
        int index = 0;
        List<XRNodeState> states = new List<XRNodeState>();
        DeviceInfo info;

        public string deviceId { get; set; }

        void Start()
        {
            info = GetComponent<DeviceInfo>();
            t = transform;
        }

        void Update()
        {
            List<XRNodeState> states = new List<XRNodeState>();
            InputTracking.GetNodeStates(states);
            //Console.WriteLine("we have " + states.Count + " nodes");
            index = info.trackerInt;

            SetPose(states);
        }

        void SetPose(List<XRNodeState> states)
        {
            if (index >= states.Count) index = 0;
            XRNodeState state = states[index];
            Vector3 trackerPosition = Vector3.zero;
            Quaternion trackerRotation = Quaternion.identity;
            if (state.TryGetRotation(out trackerRotation) && state.TryGetPosition(out trackerPosition))
            {
                if (Input.HasOrigin)
                {
                    t.rotation = (Config.rotlerp == 0 ?
                    Input.OriginRotation * trackerRotation : Quaternion.Slerp(t.rotation, Input.OriginRotation * trackerRotation, Time.unscaledDeltaTime * Config.rotlerp));

                    trackerPosition = (Config.poslerp == 0 ?
                    Input.OriginRotation * trackerPosition : Vector3.Lerp(t.position, Input.OriginRotation * trackerPosition, Time.unscaledDeltaTime * Config.poslerp));

                    for (int i = 0; i < 3; i++)
                    {
                        trackerPosition[i] *= Input.OriginScale[i];
                    }

                    t.position = trackerPosition;
                }
                else
                {
                    t.localRotation = (Config.rotlerp == 0 ?
                    trackerRotation : Quaternion.Slerp(t.localRotation, trackerRotation, Time.unscaledDeltaTime * Config.rotlerp));

                    trackerPosition = (Config.poslerp == 0 ?
                    trackerPosition : Vector3.Lerp(t.localPosition, trackerPosition, Time.unscaledDeltaTime * Config.poslerp));

                    t.localPosition = trackerPosition;
                }
            }
        }
    }
}

