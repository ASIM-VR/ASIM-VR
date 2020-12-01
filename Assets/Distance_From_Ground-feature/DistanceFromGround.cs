using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class DistanceFromGround : MonoBehaviour
{

    //Contains the text that is displayed for user when screens distance from ground is calculated
    [SerializeField]
    private TextMeshProUGUI distanceText;
    
    //VR controller that we use to control the ray
    [SerializeField]
    private XRRayInteractor controllerRay;

    [SerializeField]
    private XRController controller;

    private float distanceToGround;

    private float distanceToScreenCenter;

    private float groundPosition;

    bool GetXRInputPress() 
    {
        bool value = false;
        bool pressed = controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out value);
        Debug.Log("PRESSED!");
        return pressed && value;
    }



    //Function that is called everytime function Update() is called.
    void ScreenDistanceFromGround() 
    {
        RaycastHit hit;
        controllerRay.GetCurrentRaycastHit(out hit);

        if(hit.transform == true && hit.transform.gameObject.CompareTag("Screen") && Input.GetMouseButtonDown(0) || GetXRInputPress()) {
            CalculateDistance(hit);
        }
    }

    void CalculateDistance(RaycastHit hit) 
    {
        //Center of the screens distance to ground
        distanceToScreenCenter = hit.transform.position.y;

        //Grounds Y position
        groundPosition = GameObject.FindWithTag("Ground").transform.position.y;

        //Calculate how far from the ground is bottom of the screen
        distanceToGround = distanceToScreenCenter - hit.transform.gameObject.GetComponent<Renderer>().bounds.size.y / 2 - groundPosition;

        distanceText.SetText(hit.transform.gameObject.name + " is " + distanceToGround.ToString("F2") + " meters from the ground");

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool value)) {
            distanceText.SetText("");
        }
        ScreenDistanceFromGround();
    }
}
