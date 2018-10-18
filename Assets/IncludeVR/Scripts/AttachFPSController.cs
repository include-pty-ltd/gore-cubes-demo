using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;
using Include.VR.Viewer.Networking;

namespace Include.UnityScript
{
    public class AttachFPSController : MonoBehaviour
    {

        [SerializeField]
        float xSensitivity;
        [SerializeField]
        float ySensitivity;
        [SerializeField]
        float movementSpeed;

        private void Start()
        {
            ViewerInterface.OnDeviceConnected.AddListener(OnClientConnected);
        }

        void OnClientConnected(DeviceInfo info, GameObject client)
        {
            if (!info.Capabilities.IsCapableOf(Capability.InternalTracking) && !info.Capabilities.IsCapableOf(Capability.ExternalTracking))
            {
                Include.UnityScript.FPSCameraController fps = client.AddComponent<Include.UnityScript.FPSCameraController>();
                fps.xSensitivity = xSensitivity;
                fps.ySensitivity = ySensitivity;
                fps.movementSpeed = movementSpeed;
            }
        }
    }
}