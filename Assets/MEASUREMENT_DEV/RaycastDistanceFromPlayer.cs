using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class RaycastDistanceFromPlayer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI distanceText;

    [SerializeField]
    private XRRayInteractor controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetDistance();
    }

    void GetDistance()
    {
        RaycastHit ray;
        controller.GetCurrentRaycastHit(out ray);
        float distance = ray.distance;
        if (distance == 0)
        {
            distanceText.SetText("No target");
        }
        else 
        {
        distanceText.SetText(ray.distance.ToString("#.00") + "m");
        }
    }

    void ToggleRaycast() 
    {

    }
}
