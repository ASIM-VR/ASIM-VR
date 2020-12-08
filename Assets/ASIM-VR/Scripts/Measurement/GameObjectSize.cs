using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GameObjectSize : Tool
{
    public override AsimTool Type => AsimTool.ObjectSize;

    //VR controller that we use to control the ray
    [SerializeField]
    private XRRayInteractor controllerRay;

    [SerializeField]
    private XRController controller;

    private bool rightHandLastState;

    private void Start()
    {
        rightHandLastState = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggered) && triggered)
        {
            if(triggered != rightHandLastState)
            {
                Debug.Log("SizeCalculator: Calculating screens size");
                SearchCalculableObject();
                rightHandLastState = triggered;
            }
        }
        else if(controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressed) && pressed)
        {
            if(pressed != rightHandLastState)
            {
                Debug.Log("SizeCalculator: secondaryButton pressed, clearing infoDisplay");
                InfoDisplay.Instance.ClearText();
            }
        }
        else
        {
            rightHandLastState = false;
        }
    }

    private void SearchCalculableObject()
    {
        if(controllerRay.GetCurrentRaycastHit(out var hit))
        {
            //If Component that has been hit with ray has certain tag that implies that its size
            //would like to be calculated then it shows the user x, y and z as cm
            if(hit.transform.gameObject.CompareTag("Measurable"))
            {
                PrintSize(hit);
            }
        }
    }

    //function that will set text that will be shown to user to the correct values
    private void PrintSize(RaycastHit hit)
    {
        //holds x,y and z which are measures of object
        Vector3 objectSize = hit.transform.gameObject.GetComponent<Renderer>().bounds.size;

        InfoDisplay.Instance.SetText(
            hit.transform.gameObject.name + ":",
            "Width: " + objectSize.x.ToString("F2") + "m",
            "Height: " + objectSize.y.ToString("F2") + "m",
            "Depth: " + objectSize.z.ToString("F2") + "m");
    }
}