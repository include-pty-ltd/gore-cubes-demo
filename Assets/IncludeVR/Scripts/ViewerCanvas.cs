using Include;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    public class ViewerCanvas : MonoBehaviour
    {

        Canvas canvas;
        ViewerCameraController controller;

        void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        public void AttachListener(ViewerCameraController controller)
        {
            controller.OnStartCamera.AddListener(OnStartStream);
        }

        public void OnStartStream(ViewerDataType type, Camera camera)
        {
            if (type != ViewerDataType.Scene) return;

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = camera;
            canvas.planeDistance = 0.001f;
            canvas.renderMode = RenderMode.WorldSpace;
        }

        private void OnDestroy()
        {
            if (controller != null) controller.OnStartCamera.RemoveListener(OnStartStream);
        }
    }
}