using Include.VR.ViewR.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace Include.UnityScript
{
    class AttachSteamVRTracker : MonoBehaviour
    {
        Type SteamVRTracker = null;
        private void Start()
        {
            ViewRInterface.OnDeviceConnected.AddListener(OnConnect);
        
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if(assembly.GetName().Name.Split(',')[0] == "Assembly-CSharp")
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if(type.Name == "SteamVR_TrackedObject")
                        {
                            SteamVRTracker = type;
                            break;
                        }
                    }
                }
            }

            if(SteamVRTracker == null)
            {
                Logger.Log("AttachSteamVRTracker.Start: didn't find steamvr component");
            } else
            {
                Logger.Log("AttachSteamVRTracker.Start: found steamvr component");
            }
        }

        void OnConnect(DeviceInfo info, GameObject client)
        {
            if (!enabled) return;
            if (info.Capabilities.IsCapableOf(Capability.ExternalTracking))
            {
                Logger.Log("AttachSteamVRTracker.OnConnect: attaching steam vr tracker to device object");
                if (client == null) Logger.Log("AttachSteamVRTracker.OnConnect: client was null");
                Component tracker = client.AddComponent(SteamVRTracker);
                if(tracker == null) Logger.Log("AttachSteamVRTracker.OnConnect: tracker was null");
                FieldInfo field = SteamVRTracker.GetField("origin");
                if (field == null) Logger.Log("AttachSteamVRTracker.OnConnect: field was null");
                if(ViewRInterface.GetInstance() == null) return;
                field.SetValue(tracker, ViewRInterface.GetInstance().Origin);

                CycleSteamVRTrackers cycler = client.AddComponent<CycleSteamVRTrackers>();
                cycler.setIndex = SteamVRTracker.GetMethod("SetDeviceIndex");
                Logger.Log("SteamVR entry points found.");
                cycler.tracker = tracker;
                Logger.Log("AttachSteamVRTracker.OnConnect: finished attaching steam vr tracker to camera");

                UpdateSteamVROrigin posUpdater = client.AddComponent<UpdateSteamVROrigin>();
                posUpdater.field = field;
                posUpdater.tracker = tracker;
                Logger.Log("AttachSteamVRTracker.OnConnect: finished setting field and tracker for updater");
            }
        }
    }
}
