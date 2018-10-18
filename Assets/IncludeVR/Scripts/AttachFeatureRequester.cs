using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;

namespace Include.UnityScript
{
    public class AttachFeatureRequester : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            ViewerInterface.OnDeviceConnected.AddListener(OnClientConnected);
        }

        void OnClientConnected(DeviceInfo info, GameObject client)
        {
            Logger.Log("AttachFeatureRequester.OnClientConnected: Adding Feature Requester");
            client.AddComponent<RequestFeatures>();
        }
    }
}