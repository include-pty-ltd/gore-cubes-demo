using Include.VR.ViewR.Networking;
using System;
using UnityEngine;

namespace Include.UnityScript
{
    class AttachUnityXRTracker : MonoBehaviour
    {
        private void Start()
        {
            ViewRInterface.OnDeviceConnected.AddListener(OnConnect);
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
