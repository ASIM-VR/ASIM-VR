using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistanceFromGround : Tool
{
    public override AsimTool Type => AsimTool.DistanceFromGround;
    public override string ToolName => "Distance from ground";

    [SerializeField]
    private LineDrawer lineDrawer;

    private float screenHeight;

    //These vectors are used to draw line between the screen and ground
    private Vector3 startPosition;

    private Vector3 ground;

    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, OnTriggerDown);
        AsimInput.Instance.AddListener(InputHelpers.Button.SecondaryButton, AsimState.Down, OnClear);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, OnTriggerDown);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.SecondaryButton, AsimState.Down, OnClear);
    }

    private void OnTriggerDown(XRController controller, XRRayInteractor interactor)
    {
        ScreenDistanceFromGround(interactor);
    }

    private void OnClear(XRController controller, XRRayInteractor interactor)
    {
        lineDrawer.ResetLine();
        lineDrawer.enabled = false;
        InfoDisplay.Instance.ClearText();
    }

    //Function that is called everytime function Update() is called.
    private void ScreenDistanceFromGround(XRRayInteractor interactor)
    {
        if(interactor.GetCurrentRaycastHit(out var hit) && hit.transform.gameObject.CompareTag("Screen"))
        {
            CalculateDistance(hit);
        }
    }

    private void CalculateDistance(RaycastHit hit)
    {
        Debug.Log("Object that was hit with ray: " + hit.transform.gameObject.name);

        screenHeight = hit.transform.gameObject.GetComponent<Renderer>().bounds.size.y;

        //Bottom of the screen. Where to line should begin and it ends the the ground
        startPosition = new Vector3(hit.transform.position.x, hit.transform.position.y - screenHeight / 2, hit.transform.position.z);

        RaycastHit hit2;
        if(Physics.Raycast(startPosition, transform.TransformDirection(Vector3.down), out hit2, Mathf.Infinity))
        {
            ground = hit2.point;
            lineDrawer.DrawLine(startPosition, ground);
        }

        InfoDisplay.Instance.SetText(hit.transform.gameObject.name + " is " + hit2.distance.ToString("F2") + " meters from the ground");
    }
}