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
    private LineDrawer drawer;

    [SerializeField]
    private XRRayInteractor controllerRaycast;

    [SerializeField]
    private XRController controller;

    private Vector3 point1;
    private Vector3 point2;
    private bool measurementStarted = false;
    private bool shouldResetPoints = false;

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
            playerDistanceText.SetText(ray.distance.ToString("#.00") + "m");
        }
    }


    void resetPoints()
    {
        if (shouldResetPoints)
        {
            point1 = new Vector3();
            point2 = new Vector3();
        }
        drawer.ResetLine();
        measurementStarted = false;
        drawer.enabled = false;

    }

    void CalculateDistance()
    {
        var distance = Vector3.Distance(point1, point2);
        measurementText.SetText("Distance: " + distance.ToString());
    }

    bool getXRInputPress()
    {
        bool value;
        bool pressed = controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out value);
        Debug.Log("PRESSED!");
        return pressed && value;
    }

    void handleMeasurement()
    {
        if (Input.GetMouseButtonDown(0) || getXRInputPress())
        {   
            RaycastHit rayhit;
            controllerRaycast.GetCurrentRaycastHit(out rayhit);

            if (!measurementStarted)
            {   
                drawer.enabled = true;
                point1 = rayhit.point;
                point1Text.SetText("Point 1: " + point1.ToString());
            }
            else
            {
                point2 = rayhit.point;
                point2Text.SetText("Point 2: " + point2.ToString());
            }

            if (measurementStarted)
            {
                shouldResetPoints = true;
                drawer.DrawLine(point1, point2);
                CalculateDistance();
            }

            measurementStarted = !measurementStarted;
        } else if (Input.GetMouseButtonDown(1))
        {   
            resetPoints();
        }
    }

    void Update()
    {
        GetDistance();
        handleMeasurement();
    }
}