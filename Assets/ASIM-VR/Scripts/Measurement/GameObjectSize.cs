using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class GameObjectSize : Tool
{
    public override AsimTool Type => AsimTool.ObjectSize;

    //Three variables that contain the scale (size) of an object
    [SerializeField]
    private TextMeshProUGUI widthText;
    
    [SerializeField]
    private TextMeshProUGUI lengthText;

    [SerializeField]
    private TextMeshProUGUI depthText;

    //Contains name of the gameObject when it's set.
    [SerializeField]
    private TextMeshProUGUI objectName;

    //VR controller that we use to control the ray
    [SerializeField]
    private XRRayInteractor controllerRay;

    [SerializeField]
    private XRController controller;

    //Boolean that tells if some object's value has been calcualted earlier
    private bool sizeCalculated = false;

    private bool value;

    //holds x,y and z which are measures of object
    private Vector3 objectSize;

    bool getXRInputPress() 
    {
        value = false;
        bool pressed = controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out value);
        Debug.Log("PRESSED!");
        return pressed && value;
    }

    void resetObjectSize() 
    {
        objectSize = new Vector3();
    }


    void SearchCalculableObject() 
    {

        if (sizeCalculated) 
        {
            resetObjectSize();
            sizeCalculated = false;
        } else 
        {

            RaycastHit hit;
            controllerRay.GetCurrentRaycastHit(out hit);

            if (hit.transform == true) {
                //If Component that has been hit with ray has certain tag that implies that its size 
                //would like to be calculated then it shows the user x, y and z as cm
                if(hit.transform.gameObject.CompareTag("Measurable") && Input.GetMouseButtonDown(0) || getXRInputPress()){
                    GetSize(hit);
                }

                }
            }

    }

    //function that will set text that will be shown to user to the correct values
    void GetSize(RaycastHit hit) 
    {
            objectName.SetText(hit.transform.gameObject.name + ":");

            objectSize = hit.transform.gameObject.GetComponent<Renderer>().bounds.size;
            widthText.SetText("Width: " + objectSize.x.ToString("F2") + "m");
            Debug.Log("Width: " + objectSize.x + "m");
            lengthText.SetText("Length: " + objectSize.y.ToString("F2") + "m");
            Debug.Log("Length: " + objectSize.y + "m");
            depthText.SetText("Depth: " + objectSize.z.ToString("F2") + "m");
            Debug.Log("Length: " + objectSize.z + "m");
            sizeCalculated = true;
            
    }

    // Update is called once per frame
    void Update()
    {
        //If certain button is pressed Display text will be set to empty strings
        if (Input.GetMouseButtonDown(1) || controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out value)) {
            objectName.SetText("");
            widthText.SetText("");
            lengthText.SetText("");
            depthText.SetText("");
        }
        SearchCalculableObject();
    }
}
