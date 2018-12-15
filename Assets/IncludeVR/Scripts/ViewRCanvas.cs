using Include;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    /// <summary>
    /// Controls position and display mode of canvas when a device starts streaming
    /// </summary>
    public class ViewRCanvas : MonoBehaviour
    {
        Canvas canvas;

        void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        public void AttachListener(ViewRCameraController controller)
        {
            controller.OnStartCamera.AddListener(OnStartStream);
        }

        public void OnStartStream(ViewRDataType type, Camera camera)
        {
            if (type != ViewRDataType.Scene) return;

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = camera;
            canvas.planeDistance = 0.001f;
            canvas.renderMode = RenderMode.WorldSpace;
        }
    }
}