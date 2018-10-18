using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    public class ARCoreTracker : MonoBehaviour
    {
        Transform t;
        public bool usePrediction = true;
        DeviceInfo info;
        Include.Pose pose = new Include.Pose();

        // Use this for initialization
        void Start()
        {
            info = GetComponent<DeviceInfo>();
            t = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.TryGetPose(info.DeviceId, usePrediction, ref pose))
            {
                Quaternion trackerRotation = pose.rotation;
                Vector3 trackerPosition = pose.position;

                if (Input.HasOrigin)
                {
                    t.rotation = (Config.rotlerp == 0 ?
                    Input.OriginRotation * pose.rotation : Quaternion.Slerp(t.rotation, Input.OriginRotation * trackerRotation, Time.unscaledDeltaTime * Config.rotlerp));

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

                    for(int i = 0; i < 3; i++)
                    {
                        trackerPosition[i] *= t.localScale[i];
                    }

                    t.localPosition = trackerPosition;
                }
            }
            else
            {
                Logger.Log("ARCoreTracker.Update: no new pose this loop");
            }
        }
    }
}