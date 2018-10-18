using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    public class AttachGameObject : MonoBehaviour
    {
        public GameObject objectToAttach;

        private void Start()
        {
            ViewerInterface.OnDeviceConnected.AddListener(OnClientConnected);
        }

        private void OnClientConnected(DeviceInfo clientId, GameObject client)
        {
            DeviceInfo info = client.GetComponent<DeviceInfo>();
            if (info != null)
            {
                GameObject g = Instantiate(objectToAttach);
                g.transform.parent = client.transform;
                g.name = objectToAttach.name;
            }
        }
    }
}