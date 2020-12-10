using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class DisplayAdd : Tool
{
    public override AsimTool Type => AsimTool.AddRemove;
    public override string ToolName => "Add or remove displays";

    [SerializeField]
    private Destroy display;


    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, AddDisplay);
        AsimInput.Instance.AddListener(InputHelpers.Button.PrimaryButton, AsimState.Down, FindAndRemove);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, AddDisplay);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.PrimaryButton, AsimState.Down, FindAndRemove);
    }


    private void AddDisplay(XRController controller, XRRayInteractor interactor)
    {

        Destroy current = Instantiate(display, controller.transform.position + controller.transform.forward * 2, controller.transform.rotation);

        if (interactor.GetCurrentRaycastHit(out var hit))
        {
            //Move to the current target position and offset from the surface based on the current normal.
            current.transform.position = hit.point + (hit.normal * 0.01f);
            //Align with the current surface.
            current.transform.rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            return;
        }
      
    }


    private void FindAndRemove(XRController controller, XRRayInteractor interactor)
    {
        RaycastHit hit;
        if (interactor.GetCurrentRaycastHit(out hit))
        {
            Destroy disp = hit.collider.GetComponent<Destroy>();

            if (disp != null)
            {
                disp.DestroyDisplay();
            }
        }
    }

}
