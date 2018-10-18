using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Include;

namespace Include.UnityScript
{
    class CycleSteamVRTrackers : MonoBehaviour
    {
        private int index = 0;
        private bool latch = false;

        public MethodInfo setIndex { get; set; }

        public Component tracker { get; set; }
        public string deviceId { get; set; }

        void Update()
        {
            Touch touch = Input.GetTouches(deviceId, 0);
            if (touch != null && !latch)
            {
                Debug.Log("start touch, current index is " + index);
                latch = true;
                index++;
                index %= 16;
                setIndex.Invoke(tracker, new object[] { index });
                Debug.Log("end touch, current index is " + index);
            }
            else if (latch && touch == null)
            {
                latch = false;
            }
        }
    }
}
