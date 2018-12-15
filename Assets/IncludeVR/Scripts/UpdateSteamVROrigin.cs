using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Include.UnityScript
{
    /// <summary>
    /// Monobehaviour for updating origin of SteamVR tracked device. This uses relfection to prevent dependency on SteamVR.
    /// </summary>
    class UpdateSteamVROrigin : MonoBehaviour
    {
        public FieldInfo field { get; set; }
        public Component tracker { get; set; }

        void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            Console.WriteLine("Resetting origin for steamvr");
            if (ViewRInterface.GetInstance() != null)
                field.SetValue(tracker, ViewRInterface.GetInstance().Origin);
        }
    }
}
