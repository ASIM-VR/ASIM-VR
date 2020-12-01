using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using System.Linq;

public class InputManager : MonoBehaviour
{
    [Header("Scene objects")]
    public RadialMenu radialMenu = null;

    [SerializeField]
    private XRNode xrNode = XRNode.LeftHand; //This can be changed to Right Handed-controller from the editor

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    //Get device
    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }

    private void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }

    void Update()
    {
        //If device is disconnected in the middle of play get it again
        if (!device.isValid)
        {
            GetDevice();
        }

        /*
         * If you wan to get a list of connected device's inputs, comment thís out.
         * 
        List<InputFeatureUsage> features = new List<InputFeatureUsage>();
        device.TryGetFeatureUsages(features);
        
        foreach (var feature in features)
        {
            Debug.Log("Feature - Name:" + feature.name + " | " Type:" + feature.type);
        }
        */

        //Get boolean type input when joystick/touchpad is touched and activate radial menu 
        radialMenu.Show(device.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool touch) && touch);

        //Get boolean type input when joystick/touchpad is pressed and activate specific radial section 
        InputFeatureUsage<bool> primary2DAxisClickUsage = CommonUsages.primary2DAxisClick;
        if (device.TryGetFeatureValue(primary2DAxisClickUsage, out bool press) && press)
        {
            radialMenu.ActivateHighlithedSection();
        }

        //Get Vector2 type input from joystick/touchpad when its moved and move cursor on the radial menu
        InputFeatureUsage<Vector2> primary2DAxisUsage = CommonUsages.primary2DAxis;
        if (device.TryGetFeatureValue(primary2DAxisUsage, out Vector2 touchPosition) && touchPosition != Vector2.zero)
        {
            radialMenu.SetTouchPosition(touchPosition);
            //Debug.Log("Primary2DAxisValue:" + primary2DAxisValue);
        }
    }

}