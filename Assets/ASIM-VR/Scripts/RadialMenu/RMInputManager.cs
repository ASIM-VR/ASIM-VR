using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class RMInputManager : MonoBehaviour
{
    [Header("Scene objects")]
    public RadialMenu RadialMenu = null;
    
    [SerializeField]
    private XRController controller;

    void Update()
    {


        //Get boolean type input when joystick/touchpad is touched and activate radial menu 
        RadialMenu.Show(controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool touch) && touch);

        //Get boolean type input when joystick/touchpad is pressed and activate specific radial section 
        InputFeatureUsage<bool> primary2DAxisClickUsage = CommonUsages.primary2DAxisClick;
        if (controller.inputDevice.TryGetFeatureValue(primary2DAxisClickUsage, out bool press) && press)
        {
            RadialMenu.ActivateHighlithedSection();
            //Debug.Log("primary2DAxisClickUsage:" + press);
        }

        //Get Vector2 type input from joystick/touchpad when its moved and move cursor on the radial menu
        InputFeatureUsage<Vector2> primary2DAxisUsage = CommonUsages.primary2DAxis;
        if (controller.inputDevice.TryGetFeatureValue(primary2DAxisUsage, out Vector2 touchPosition) && touchPosition != Vector2.zero)
        {
            RadialMenu.SetTouchPosition(touchPosition);
            //Debug.Log("Primary2DAxisValue:" + touchPosition);
        }
    }

}