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

        public MethodInfo setIndex { get; set; }
        public Component tracker { get; set; }
        DeviceInfo info;

        void Start()
        {
            info = GetComponent<DeviceInfo>();
        }

        void Update()
        {
            if (index != info.trackerInt)
                setIndex.Invoke(tracker, new object[] { info.trackerInt });
        }
    }
}
