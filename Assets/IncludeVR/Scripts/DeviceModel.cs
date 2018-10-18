using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    public class DeviceModel : MonoBehaviour
    {
        [SerializeField]
        private GameObject screen;

        private ViewerCameraController controller = null;

        //new DeviceCamera camera;
        private MeshRenderer r;

        void Start()
        {
            DeviceInfo info = GetComponentInParent<DeviceInfo>();
            transform.localScale = new Vector3(info.ScreenHeight / 1000, info.ScreenWidth / 1000, 0.008f);
            transform.position = transform.position - Vector3.forward * 0.008f;
            controller = info.GetComponentInChildren<ViewerCameraController>();
            if (controller != null)
            {
                controller.OnStartCamera.AddListener(StartStream);
                controller.OnStopStream.AddListener(StopStream);
            }
            r = screen.GetComponent<MeshRenderer>();
        }

        void StartStream(ViewerDataType type, Camera camera)
        {
            if (type == ViewerDataType.Scene)
                r.material.SetTexture("_MainTex", camera.targetTexture);
        }

        void StopStream()
        {
            r.material.SetTexture("_MainTex", null);
        }
    }
}