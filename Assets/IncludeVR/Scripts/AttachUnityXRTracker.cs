using Include.VR.Viewer.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Include.UnityScript
{
    class AttachUnityXRTracker : MonoBehaviour
    {
        private void Start()
        {
            ViewerInterface.OnDeviceConnected.AddListener(OnConnect);
        }

        void OnConnect(DeviceInfo info, GameObject client)
        {
            if (!enabled) return;
            if (info.Capabilities.IsCapableOf(Capability.ExternalTracking))
            {
                Console.WriteLine("attaching tracker to camera");
                UnityXRTracker tracker = client.AddComponent<UnityXRTracker>();
                tracker.deviceId = info.DeviceId;
            }
        }
    }
}
