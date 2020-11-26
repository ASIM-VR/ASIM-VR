using System.Xml;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class RaycastDistanceHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerDistanceText;
    [SerializeField]
    private TextMeshProUGUI point1Text;
    [SerializeField]
    private TextMeshProUGUI point2Text;

    [SerializeField]
    private TextMeshProUGUI measurementText;

    [SerializeField]
    private XRRayInteractor controllerRaycast;

    [SerializeField]
    private XRController controller;
    
    [SerializeField]
    private LineDrawer drawer;

    private Vector3 point1;
    private Vector3 point2;

    private bool firstPress;
    private bool secondPress;
    bool rightHandLastState; // Used to track button press state.
    
    void Start()
    {
        rightHandLastState = false;
        firstPress = false;
        secondPress = false;
    }

    private void Update()
    {
        GetDistance();

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggered) && triggered)
        {
            if (triggered != rightHandLastState)
            {
                Debug.Log("MEASURING");
                MeasureDistance();

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

    void GetDistance()
    {
        RaycastHit ray;
        controllerRaycast.GetCurrentRaycastHit(out ray);
        float distance = ray.distance;
        if (distance == 0)
        {
            playerDistanceText.SetText("No target");
        }
        else 
        {
            playerDistanceText.SetText(ray.distance.ToString("0.00") + "m");
        }
    }


    void ResetPoints()
    {
        point1 = Vector3.zero;
        point2 = Vector3.zero;
        point1Text.SetText("Point 1: " + point1.ToString());
        point2Text.SetText("Point 2: " + point2.ToString());
        measurementText.SetText("Distance: ");
        drawer.ResetLine();
        drawer.enabled = false;
    }

    void CalculateDistance()
    {
        var distance = Vector3.Distance(point1, point2);
        measurementText.SetText("Distance: " + distance.ToString("0.00") + "m");
    }

    void MeasureDistance()
    {
        // Returns boolean; turn into an if statement. RaycastHit exists only within if.
        if (controllerRaycast.GetCurrentRaycastHit(out RaycastHit rayhit))
        {
            if (!firstPress && !secondPress)
            {
                point1 = rayhit.point;
                point1Text.SetText("Point 1: " + point1.ToString());
                firstPress = true;
            }
            else if (firstPress && !secondPress)
            {
                point2 = rayhit.point;
                point2Text.SetText("Point 2: " + point2.ToString());
                CalculateDistance();
                secondPress = true;
            }
            else if (firstPress && secondPress) // BUG: ResetPoints() does not work without a target.
            {
                Debug.Log("RESETTING");
                ResetPoints();
                firstPress = false;
                secondPress = false;
            }
        }
    }
}