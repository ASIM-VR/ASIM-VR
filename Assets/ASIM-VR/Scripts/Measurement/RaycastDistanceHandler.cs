using System.Xml;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class RaycastDistanceHandler : Tool
{
    public override AsimTool Type => AsimTool.TapeMeasure;
    public override string ToolName => "Object distance calculator";

    /*    [SerializeField]
        private TextMeshProUGUI playerDistanceText;
        [SerializeField]
        private TextMeshProUGUI point1Text;
        [SerializeField]
        private TextMeshProUGUI point2Text;

        [SerializeField]
        private TextMeshProUGUI measurementText;*/

    [SerializeField]
    private XRRayInteractor controllerRaycast;

    [SerializeField]
    private XRController controller;
    
    [SerializeField]
    private LineDrawer lineDrawer;

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
        
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggered) && triggered)
        {
            if (triggered != rightHandLastState)
            {
                Debug.Log("RaycastDistanceHandler: Measuring Distance");
                MeasureDistance();

                rightHandLastState = triggered;
            }  
        }
        else if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed) && pressed)
        {
            if (pressed != rightHandLastState)
            {
                Debug.Log("RaycastDistanceHandler: primaryButton pressed");
            }
        }
        else
        {
            rightHandLastState = false;
        }


        if (Input.GetMouseButtonDown(1))
        {

            Debug.Log("hiiren klikkaus");
        
            MeasureDistance();
        }


        RenderText();

    }

    string GetDistance()
    {
        RaycastHit ray;
        controllerRaycast.GetCurrentRaycastHit(out ray);
        float distance = ray.distance;
        if (distance == 0)
        {
            return "No target";
        }
        else 
        {
            return ray.distance.ToString("0.00") + "m";
        }
    }


    void ResetPoints()
    {
        point1 = Vector3.zero;
        point2 = Vector3.zero;
        lineDrawer.ResetLine();
        lineDrawer.enabled = false;
    }

    string GetCalculatedDistance()
    {
        var distance = Vector3.Distance(point1, point2);
        return "Distance: " + distance.ToString("0.00") + "m";
    }

    void MeasureDistance()
    {


        if (firstPress && secondPress) // BUG: ResetPoints() does not work without a target.
        {
            Debug.Log("RaycastDistanceHandler: resetting");
            ResetPoints();
            firstPress = false;
            secondPress = false;
        }

        // Returns boolean; turn into an if statement. RaycastHit exists only within if.
        else if (controllerRaycast.GetCurrentRaycastHit(out RaycastHit rayhit))
        {
            if (!firstPress && !secondPress)
            {
                point1 = rayhit.point;
                firstPress = true;
            }
            else if (firstPress && !secondPress)
            {
                point2 = rayhit.point;
                secondPress = true;
                lineDrawer.DrawLine(point1, point2);
            }
    
        }
        
    }

    void RenderText()
    {

        Debug.Log(firstPress + " " + secondPress);

        if (firstPress && !secondPress)
        {
            InfoDisplay.Instance.SetText(
                GetDistance(),
                "Point 1: " + point1.ToString()
                );
        } 
        else if (firstPress && secondPress) 
        {
            var distance = Vector3.Distance(point1, point2);

            InfoDisplay.Instance.SetText(
                GetDistance(),
                "Point 1: " + point1.ToString(),
                "Point 2: " + point2.ToString(),
                GetCalculatedDistance()
            );
        }
        else
        {
            InfoDisplay.Instance.SetText(GetDistance());
        }

    }

}