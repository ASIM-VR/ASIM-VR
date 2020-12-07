using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class InputTest : MonoBehaviour
{
    [SerializeField]
    private XRController controller;
    bool rightHandLastState; // Used to track button presses

    void Start()
    {
        rightHandLastState = false;
    }

    private void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggered) && triggered)
        {
            if (triggered != rightHandLastState)
            {
                Debug.Log("TRIGGERED");
                rightHandLastState = triggered;
            }  
        }
        else if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed) && pressed)
        {
            if (pressed != rightHandLastState)
            {
                Debug.Log("PRESSED");
            }
        }
        else
        {
            rightHandLastState = false;
        }
    }
}
