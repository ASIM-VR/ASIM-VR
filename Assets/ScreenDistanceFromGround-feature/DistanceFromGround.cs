using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class DistanceFromGround : MonoBehaviour
{
    
    //VR controller that we use to control the ray
    [SerializeField]
    private XRRayInteractor controllerRay;

    [SerializeField]
    private XRController controller;

    [SerializeField]
    private LineDrawer lineDrawer;

    private float distanceToGround;

    private float distanceToScreenCenter;

    private float groundPosition;

    private float screenHeight;

    bool rightHandLastState; // Used to track button press state.

    //These vectors are used to draw line between the screen and ground
    private Vector3 screenBottom;
    private Vector3 ground;
 

    private void Start() 
    {
        rightHandLastState = false;
    }


    // Update is called once per frame
    private void Update()
    {

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggered) && triggered)
        {
            if (triggered != rightHandLastState)
            {
                Debug.Log("DistanceFromGround: calculating distance from bottom of the screen to ground");
                ScreenDistanceFromGround();

                rightHandLastState = triggered;
            }  
        }
        else if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressed) && pressed)
        {
            if (pressed != rightHandLastState)
            {
                Debug.Log("DistanceFromGround: secondaryButton pressed, clearing infoDisplay and clearing the line");
                lineDrawer.ResetLine();
                lineDrawer.enabled = false;
                InfoDisplay.Instance.ClearText();
            }
        }
        else
        {
            rightHandLastState = false;
        }

    }
            

    //Function that is called everytime function Update() is called.
    void ScreenDistanceFromGround() 
    {
        RaycastHit hit;
        controllerRay.GetCurrentRaycastHit(out hit);

        if(hit.transform == true && hit.transform.gameObject.CompareTag("Screen")) {
            CalculateDistance(hit);
        }
    }

    void CalculateDistance(RaycastHit hit) 
    {
        //screen center y component
        distanceToScreenCenter = hit.transform.position.y;

        Debug.Log(hit.transform.gameObject.name);

        //Grounds Y position
        groundPosition = GameObject.FindWithTag("Ground").transform.position.y;

        screenHeight = hit.transform.gameObject.GetComponent<Renderer>().bounds.size.y;

        //Calculate how far from the ground is bottom of the screen
        distanceToGround = distanceToScreenCenter - screenHeight / 2 - groundPosition;

        //Setting correct values for points that we use to draw a line
        screenBottom = new Vector3(hit.transform.position.x, hit.transform.position.y - screenHeight / 2, hit.transform.position.z);
        ground = new Vector3(hit.transform.position.x, hit.transform.position.y - distanceToGround, hit.transform.position.z);

        lineDrawer.DrawLine(screenBottom, ground);

        InfoDisplay.Instance.SetText(hit.transform.gameObject.name + " is " + distanceToGround.ToString("F2") + " meters from the ground");

    }
}
