using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InputManager : MonoBehaviour
{
    [Header("Actions")]
    private bool touch;
    private bool press;
    private Vector2 touchPosition = Vector2.zero;

    [Header("Scene objects")]
    public RadialMenu radialMenu = null;

    [SerializeField]
    private XRNode xrNode = XRNode.LeftHand; //This can be changed to Right Handed-controller from the editor

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;
    
    //Get device
    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault(); //Linq?
    }
    
    private void OnEnable()
    {
        if(!device.isValid)
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
        InputFeatureUsage<bool> primary2DAxisTouchUsage = CommonUsages.primary2DAxisTouch;
        if (device.TryGetFeatureValue(primary2DAxisTouchUsage, out touch) && touch)
        {
            //Debug.Log("primary2DAxisTouch:" + touch);
            radialMenu.Show(touch);
            
        }
        else
        {
            touch = false;
            radialMenu.Show(touch);
        }

        //Get boolean type input when joystick/touchpad is pressed and activate specific radial section 
        InputFeatureUsage<bool> primary2DAxisClickUsage = CommonUsages.primary2DAxisClick;
        if (device.TryGetFeatureValue(primary2DAxisClickUsage, out press) && press)
        {
            radialMenu.ActivateHighlithedSection();
        }
        else
        {
            press = false;
        }

        //Get Vector2 type input from joystick/touchpad when its moved and move cursor on the radial menu
        InputFeatureUsage<Vector2> primary2DAxisUsage = CommonUsages.primary2DAxis;
        if(device.TryGetFeatureValue(primary2DAxisUsage, out touchPosition) && touchPosition != Vector2.zero)
        {
            radialMenu.SetTouchPosition(touchPosition);
            //Debug.Log("Primary2DAxisValue:" + primary2DAxisValue);
        }
    }

}