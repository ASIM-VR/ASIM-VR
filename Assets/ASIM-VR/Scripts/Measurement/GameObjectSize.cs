using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameObjectSize : Tool
{
    public override AsimTool Type => AsimTool.ObjectSize;
    public override string ToolName => "Object size calculator";

    private bool showingText;

    private void OnEnable()
    {
        showingText = false;
        AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, FindTarget);
    }

    private void FindTarget(XRController controller, XRRayInteractor interactor)
    {
        if(showingText)
        {
            InfoDisplay.Instance.ClearText();
            showingText = false;
            return;
        }
        SearchCalculableObject(interactor);
    }

    private void SearchCalculableObject(XRRayInteractor controllerRay)
    {
        if(controllerRay.GetCurrentRaycastHit(out var hit))
        {
            //If Component that has been hit with ray has certain tag that implies that its size
            //would like to be calculated then it shows the user x, y and z as cm
            if(hit.transform.gameObject.CompareTag("Measurable"))
            {
                PrintSize(hit);
                showingText = true;
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
            $"Width: {objectSize.x:F2}m",
            $"Height: {objectSize.y:F2}m",
            $"Depth: {objectSize.z:F2}m");
    }
}