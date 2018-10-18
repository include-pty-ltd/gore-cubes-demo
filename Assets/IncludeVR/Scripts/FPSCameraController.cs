using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Include.UnityScript
{
    public class FPSCameraController : MonoBehaviour
    {
        public float xSensitivity { get; set; }
        public float ySensitivity { get; set; }
        public float movementSpeed { get; set; }

        float totalX;
        float totalY;

        void MouseRotate()
        {
            //get rotation
            float x = xSensitivity * UnityEngine.Input.GetAxis("Mouse X");
            float y = ySensitivity * UnityEngine.Input.GetAxis("Mouse Y");

            //change rotation
            totalX += x;
            totalY += y;

            //clamp
            if (totalY > 90)
            {
                totalY = 90;
            }
            else if (totalY < -90)
            {
                totalY = -90;
            }

            //prevent overflow
            totalX %= 360;

            //transform
            this.transform.rotation = Quaternion.Euler(totalY, totalX, 0);
        }

        void KeyboardMovement()
        {
            Vector3 movement = Vector3.zero;
            float deltaTime = Time.deltaTime;

            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                movement.z += deltaTime;
            }
            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                movement.z -= deltaTime;
            }
            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                movement.x -= deltaTime;
            }
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                movement.x += deltaTime;
            }

            movement.Normalize();
            movement *= movementSpeed;

            this.transform.position += this.transform.rotation * movement;
        }

        // Update is called once per frame
        void Update()
        {
            if (UnityEngine.Input.GetMouseButton(1))
            {
                Debug.Log("Button down");
                Cursor.lockState = CursorLockMode.Locked;
                MouseRotate();
                KeyboardMovement();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
}