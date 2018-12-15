using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Include;
using Include.VR.ViewR.Networking;
using Include.VR.ViewR.Networking.Messages;


// this is an example on how to turn device capabilities on and off from unity
// please don't actually use this monobehaviour in your project
namespace Include.UnityScript
{
    public class RequestFeatures : MonoBehaviour
    {
        [SerializeField]
        bool MotionSensors;
        bool currentMotionSensors = false;
        [SerializeField]
        bool InternalTracking;
        bool currentInternalTracking = false;
        [SerializeField]
        bool ExternalTracking;
        bool currentExternalTracking = false;
        [SerializeField]
        bool FrontCamera;
        bool currentFrontCamera = false;
        [SerializeField]
        bool BackCamera;
        bool currentBackCamera = false;
        [SerializeField]
        bool ExternalCamera;
        bool currentExternalCamera = false;
        [SerializeField]
        bool Vibration;
        bool currentVibration = false;
        [SerializeField]
        bool Microphone;
        bool currentMicrophone = false;
        [SerializeField]
        bool Speaker;
        bool currentSpeaker = false;
        [SerializeField]
        bool Keyboard;
        bool currentKeyboard = false;
        [SerializeField]
        bool TouchScreen;
        bool currentTouchScreen = false;

        DeviceInfo info;
        Capabilities c;

        private void Start()
        {
            Logger.Log("RequestFeatures.Start: Started");
            info = GetComponent<DeviceInfo>();
            c = info.Capabilities;

            Logger.Log("RequestFeatures.Start: Summoning Delayed request");
            StartCoroutine("RequestCapabilities");
        }

        IEnumerator RequestCapabilities()
        {
            Logger.Log("RequestFeatures.RequestCapabilities: waiting 2s");
            yield return new WaitForSecondsRealtime(2);
            TouchScreen = true;
            InternalTracking = true;
            Logger.Log("RequestFeatures.RequestCapabilities: set values");
        }

        private void Update()
        {
            if (MotionSensors != currentMotionSensors && c.IsCapableOf(Capability.MotionSensors))
            {
                currentMotionSensors = MotionSensors;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.MotionSensors, MotionSensors, PrintResponse);
            }
            if (InternalTracking != currentInternalTracking && c.IsCapableOf(Capability.InternalTracking))
            {
                currentInternalTracking = InternalTracking;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.InternalTracking, InternalTracking, PrintResponse);
            }
            if (ExternalTracking != currentExternalTracking && c.IsCapableOf(Capability.ExternalTracking))
            {
                currentExternalTracking = ExternalTracking;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.ExternalTracking, ExternalTracking, PrintResponse);
            }
            if (FrontCamera != currentFrontCamera && c.IsCapableOf(Capability.FrontCamera))
            {
                currentFrontCamera = FrontCamera;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.FrontCamera, FrontCamera, PrintResponse);
            }
            if (BackCamera != currentBackCamera && c.IsCapableOf(Capability.BackCamera))
            {
                currentBackCamera = BackCamera;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.BackCamera, BackCamera, PrintResponse);
            }
            if (ExternalCamera != currentExternalCamera && c.IsCapableOf(Capability.ExternalCamera))
            {
                currentExternalCamera = ExternalCamera;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.ExternalCamera, ExternalCamera, PrintResponse);
            }
            if (Vibration != currentVibration && c.IsCapableOf(Capability.Vibration))
            {
                currentVibration = Vibration;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.Vibration, Vibration, PrintResponse);
            }
            if (Microphone != currentMicrophone && c.IsCapableOf(Capability.Microphone))
            {
                currentMicrophone = Microphone;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.Microphone, Microphone, PrintResponse);
            }
            if (Speaker != currentSpeaker && c.IsCapableOf(Capability.Speaker))
            {
                currentSpeaker = Speaker;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.Speaker, Speaker, PrintResponse);
            }
            if (Keyboard != currentKeyboard && c.IsCapableOf(Capability.Keyboard))
            {
                currentKeyboard = Keyboard;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.Keyboard, Keyboard, PrintResponse);
            }
            if (TouchScreen != currentTouchScreen && c.IsCapableOf(Capability.TouchScreen))
            {
                currentTouchScreen = TouchScreen;
                ViewRInterface.RequestCapability(info.DeviceId, Capability.TouchScreen, TouchScreen, PrintResponse);
            }
        }

        void PrintResponse(CapabilityResponse response)
        {
            Debug.Log("Response: " + response.Capability + " " + response.Status);
        }
    }
}