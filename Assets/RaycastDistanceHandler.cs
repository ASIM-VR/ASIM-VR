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

    private Vector3 point1;
    private Vector3 point2;
    private bool measurementStarted = false;
    private bool shouldResetPoints = false;

    void handleCast()
    {
        RaycastHit ray;
        controllerRaycast.GetCurrentRaycastHit(out ray);
        playerDistanceText.SetText(ray.distance.ToString());
    }


    void resetPoints()
    {
        if (shouldResetPoints)
        {
            point1 = new Vector3();
            point2 = new Vector3();
        }
    }


    void CalculateDistance()
    {
        Debug.Log("distance points: " + point1 + " | " + point2);
        var distance = Vector3.Distance(point1, point2);
        Debug.Log("Distance = " + distance.ToString());
        measurementText.SetText(distance.ToString());
    }



    bool getXRInputPress()
    {
        bool value;
        return controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out value) && value;
    }

    void handleMeasurement()
    {
        if (Input.GetMouseButtonDown(0) || getXRInputPress())
        {
            resetPoints();
            RaycastHit rayhit;
            controllerRaycast.GetCurrentRaycastHit(out rayhit);

            if (!measurementStarted)
            {
                point1 = rayhit.point;
                point1Text.SetText(point1.ToString());
            }
            else
            {
                point2 = rayhit.point;
                point2Text.SetText(point2.ToString());
            }

            if (measurementStarted)
            {
                shouldResetPoints = true;
                CalculateDistance();
            }

            measurementStarted = !measurementStarted;
        }
    }


    void Start()
    {
        handleCast();
    }

    void Update()
    {
        handleCast();

        handleMeasurement();

    }
}
