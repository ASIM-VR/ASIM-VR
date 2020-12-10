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

    private float screenHeight;

    bool rightHandLastState; // Used to track button press state.

    //These vectors are used to draw line between the screen and ground
    private Vector3 startPosition;
    private Vector3 ground;
 

    private void Start() 
    {
        rightHandLastState = false;
    }


    // Update is called once per frame
    private void Update()
    {

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggered) && triggered )
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
        else if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Clicked left click");
            ScreenDistanceFromGround();
        }
        else
        {
            rightHandLastState = false;
        }

    }
            

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
        Debug.Log("Object that was hit with ray: " + hit.transform.gameObject.name);

        screenHeight = hit.transform.gameObject.GetComponent<Renderer>().bounds.size.y;

        //Bottom of the screen. Where to line should begin and it ends the the ground
        startPosition = new Vector3(hit.transform.position.x, hit.transform.position.y - screenHeight / 2, hit.transform.position.z);

        RaycastHit hit2;
        if(Physics.Raycast(startPosition, transform.TransformDirection(Vector3.down), out hit2, Mathf.Infinity)) {
            ground = hit2.point;
            lineDrawer.DrawLine(startPosition, ground);        
        }

        InfoDisplay.Instance.SetText(hit.transform.gameObject.name + " is " + hit2.distance.ToString("F2") + " meters from the ground");

    }
}
