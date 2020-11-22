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

    void GetDistance()
    {
        if (controllerRaycast.GetCurrentRaycastHit(out var hit)){

            float distance = hit.distance;
            if (distance == 0)
            {
                playerDistanceText.SetText("No target");
            }
            else 
            {
                playerDistanceText.SetText(distance.ToString("#.00") + "m");
            }
        }
    }


    void ResetPoints()
    {
        point1 = new Vector3();
        point2 = new Vector3();
        drawer.ResetLine();
        measurementStarted = false;
        drawer.enabled = false;
    }

    void CalculateDistance()
    {
        var distance = Vector3.Distance(point1, point2);
        measurementText.SetText("Distance: " + distance.ToString());
    }

    bool GetXRInputPress()
    {
        return controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value;
    }

    void HandleMeasurement()
    {
        if (Input.GetMouseButtonDown(0) || GetXRInputPress())
        {   

            if (controllerRaycast.GetCurrentRaycastHit(out var hit) && !measurementStarted)
            {   
                drawer.enabled = true;
                point1 = hit.point;
                point1Text.SetText("Point 1: " + point1.ToString());
            }
            else
            {
                point2 = hit.point;
                point2Text.SetText("Point 2: " + point2.ToString());
                drawer.DrawLine(point1, point2);
                CalculateDistance();
            }

            measurementStarted = !measurementStarted;
        } else if (Input.GetMouseButtonDown(1))
        {   
            ResetPoints();
        }
    }

    void Update()
    {
        GetDistance();
        HandleMeasurement();
    }
}