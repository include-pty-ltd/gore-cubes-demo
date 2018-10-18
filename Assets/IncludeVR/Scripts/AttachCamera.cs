﻿using Include;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Include.UnityScript
{
    public class AttachCamera : MonoBehaviour
    {
        //this is the looking glass camera object to be spawned
        public GameObject cameraObject;

        //these are ui elements
        public GameObject[] canvasObjects;

        private void Start()
        {
            ViewerInterface.OnDeviceConnected.AddListener(OnClientConnected);
        }

        void OnClientConnected(DeviceInfo info, GameObject client)
        {
            GameObject cam = Instantiate(cameraObject);
            cam.transform.parent = client.transform;
            cam.name = cameraObject.name;

            ViewerCameraController controller = cam.GetComponent<ViewerCameraController>();

            foreach (GameObject go in canvasObjects)
            {
                GameObject c = Instantiate(go);
                c.AddComponent<ViewerCanvas>().AttachListener(controller);
                c.transform.parent = client.transform;
                c.name = go.name;
            }
        }


    }
}