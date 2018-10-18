using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SetTemplateCameraTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Assembly[] assembs = System.AppDomain.CurrentDomain.GetAssemblies();

        Type t = Type.GetType("Include.LookingGlassScripts.AttachCamera");
        if (t == null)
        {
            Debug.Log("Type was null, getting it manually");
            foreach (Assembly a in assembs)
            {
                if (a.FullName.Split(',')[0] == "LookingGlassScripts")
                {
                    Type[] ts = a.GetTypes();
                    foreach (Type type in ts)
                    {
                        if (type.Name == "AttachCamera")
                        {
                            t = type;
                        }
                    }
                }
            }
        }
        Debug.Log(t);
        GameObject listeners = GameObject.Find("/LookingGlassInterface/Listeners");
        ConfigureLookingGlassForBeatsaber(listeners);
    }

    void ConfigureLookingGlassForBeatsaber(GameObject gameObject)
    {
        Debug.Log("Configuring Looking Glass For Beatsaber");
        Assembly[] assembs = System.AppDomain.CurrentDomain.GetAssemblies();
        bool templateCameraSet = false;
        bool xrTrackingSet = false;
        foreach (Assembly a in assembs)
        {
            if (a.FullName.Split(',')[0] == "LookingGlassScripts")
            {
                Type[] ts = a.GetTypes();
                foreach (Type type in ts)
                {
                    if (type.Name == "AttachCamera")
                    {
                        Debug.Log("Found attach camera script in dll");
                        Component c = gameObject.GetComponent(type);
                        if (c == null) continue;
                        PropertyInfo prop = type.GetProperty("templateCamera");
                        if (prop == null) continue;
                        Camera cam = Camera.main;
                        if (cam == null) continue;
                        prop.SetValue(c, cam.gameObject, null);
                        Debug.Log("Successfully Set Camera Template");
                        if (xrTrackingSet) return;
                        templateCameraSet = true;
                    }
                    if (type.Name == "AttachUnityXRTracker")
                    {
                        Debug.Log("Found attach tracker script in dll");
                        Component c = gameObject.GetComponent(type);
                        if (c == null) continue;
                        PropertyInfo prop = type.GetProperty("enabled");
                        if (prop == null) continue;
                        prop.SetValue(c, true, null);
                        Debug.Log("Successfully Enabled Unity XR Tracking");
                        if (templateCameraSet) return;
                        xrTrackingSet = true;
                    }
                }
            }
        }
    }
}
