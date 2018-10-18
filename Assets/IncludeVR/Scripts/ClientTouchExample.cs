using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;

// this script shows how to use 
namespace Include.UnityScript
{
    public class ClientTouchExample : MonoBehaviour
    {
        public GameObject cursorPrefab;
        public int cursorCount = 10;
        public float maxDistance = 10f;
        public bool eventBased;

        new Camera camera;

        List<CursorController> cursors = new List<CursorController>();
        Dictionary<int, Vector2> touchPositions = new Dictionary<int, Vector2>();
        List<int> toRemove = new List<int>();

        string clientId;
        Ray ray;
        Vector3 hitPoint;
        RaycastHit[] hits;

        private void Start()
        {
            GameObject go = new GameObject();
            go.name = "Cursors";
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            go.transform.parent = transform;

            if (cursorPrefab == null)
            {
                cursorPrefab = (GameObject)Resources.Load("Prefabs/Cursor", typeof(GameObject));
            }

            float hue = Random.Range(0f, 1f);
            for (int i = 0; i < cursorCount; i++)
            {
                GameObject cursor = Instantiate(cursorPrefab, go.transform);
                cursor.name = "cursor " + i;
                CursorController cursorController = cursor.GetComponent<CursorController>();
                cursorController.color = Color.HSVToRGB(i / (float)cursorCount, 1, 1);
                cursors.Add(cursorController);

                touchPositions.Add(i, new Vector2());
            }

            DeviceInfo info = GetComponent<DeviceInfo>();
            clientId = info.DeviceId;
            camera = GetComponentInChildren<Camera>();

            Include.Input.TouchEvent.AddListener(OnTouch);
        }

        void OnTouch(TouchEventArgs args)
        {

            if (args.deviceId != clientId)
                return;

            for (int i = 0; i < cursorCount; i++)
            {
                if (args.touches.ContainsKey(i) && args.touches[i].phase != TouchPhase.Ended && args.touches[i].phase != TouchPhase.Canceled)
                {
                    touchPositions[i] = args.touches[i].rawPosition;
                }
                else
                {
                    touchPositions[i] = Vector2.zero;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            Dictionary<int, Include.Touch> touches = null;
            if (!eventBased)
            {
                touches = Include.Input.GetTouches(clientId);
            }

            for (int i = 0; i < 10; i++)
            {
                Vector2 v = Vector2.zero;
                if (eventBased)
                {
                    v = touchPositions[i];
                }
                else if (touches != null && touches.ContainsKey(i))
                {
                    v = touches[i].rawPosition;
                    if (touches[i].phase == TouchPhase.Canceled || touches[i].phase == TouchPhase.Ended)
                    {
                        toRemove.Add(i);
                    }
                }


                if (v != Vector2.zero)
                {
                    cursors[i].visible = true;
                    ray = camera.ViewportPointToRay(v);
                    hitPoint = ray.GetPoint(maxDistance);
                    hits = Physics.RaycastAll(ray);
                    float distance = maxDistance;
                    foreach (var hit in hits)
                    {
                        if (hit.distance < distance)
                        {
                            hitPoint = hit.point;
                            distance = hit.distance;
                        }
                    }
                    cursors[i].transform.position = hitPoint;
                }
                else
                {
                    cursors[i].visible = false;
                }

                if (eventBased)
                {
                    foreach (int j in toRemove)
                    {
                        touches.Remove(j);
                    }
                    toRemove.Clear();
                }
            }
        }
    }
}