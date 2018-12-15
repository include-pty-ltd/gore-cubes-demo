using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Include.UnityScript
{
    public class Button : MonoBehaviour
    {
        UnityEngine.UI.Button button;
        DeviceInfo info;
        string deviceId;
        new Camera camera;
        ViewRCameraController controller;
        

        // Use this for initialization
        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            info = GetComponentInParent<DeviceInfo>();
            if (info != null) deviceId = info.DeviceId;

            Input.RegisterTouchable(gameObject);

            controller = info.GetComponentInChildren<ViewRCameraController>();
            controller.OnStartCamera.AddListener(OnStartStream);
        }

        void OnStartStream(ViewRDataType type, Camera camera)
        {
            if (type == ViewRDataType.Scene)
                this.camera = camera;
        }

        // Update is called once per frame
        void Update()
        {
            Touch touch = null;
            if (info != null) touch = Input.GetTouches(deviceId, 0);
            if (touch != null && touch.phase == TouchPhase.Began && touch.touchedObject == gameObject)
            {
                button.onClick.Invoke();
            }
        }
    }
}
