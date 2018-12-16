using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;
using Include.VR.ViewR.Networking;

namespace Include.UnityScript
{
    public class AttachARCoreTracker : MonoBehaviour
    {
        private void Start()
        {
            Logger.Log("AttachARCoreTracker.Start: Registering Listener");
            ViewRInterface.OnDeviceConnected.AddListener(OnConnect);
        }

        void OnEnable()
        {
            Logger.Log("AttachARCoreTracker.OnEnable: Enabled");
        }

        void OnConnect(DeviceInfo info, GameObject client)
        {
            Debug.Log(info.Capabilities);

            if (info.Capabilities.IsCapableOf(Capability.InternalTracking))
            {
                Logger.Log("AttachARCoreTracker.OnConnect: Attaching Tracker");
                ARCoreTracker tracker = client.AddComponent<ARCoreTracker>();
                if(tracker == null)
                {
                    Logger.Log("AttachARCoreTracker.OnConnect: why the hell is this null?");
                } else
                {
                    Logger.Log("AttachARCoreTracker.OnConnect: tracker attached");
                }

            }
        }
    }
}