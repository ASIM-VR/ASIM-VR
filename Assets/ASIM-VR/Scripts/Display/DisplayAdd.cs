using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class DisplayAdd : Tool
{
    public override AsimTool Type => AsimTool.AddRemove;
    public override string ToolName => "Add or remove displays";

    [SerializeField]
    private Destroy display;

    private float resizeStartLength;

    private Dictionary<XRNode, (Transform target, Vector3 point)> resizePoints;

    private void Awake()
    {
        resizePoints = new Dictionary<XRNode, (Transform target, Vector3 point)>();
    }

    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, AddDisplay);
        AsimInput.Instance.AddListener(InputHelpers.Button.PrimaryButton, AsimState.Down, FindAndRemove);
        AsimInput.Instance.AddListener(InputHelpers.Button.Grip, AsimState.Down, ControllerGripDown);
        AsimInput.Instance.AddListener(InputHelpers.Button.Grip, AsimState.Hold, ControllerGripHold);
        AsimInput.Instance.AddListener(InputHelpers.Button.Grip, AsimState.Up, ControllerGripUp);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, AddDisplay);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.PrimaryButton, AsimState.Down, FindAndRemove);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Grip, AsimState.Down, ControllerGripDown);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Grip, AsimState.Hold, ControllerGripHold);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Grip, AsimState.Up, ControllerGripUp);
    }



    private void ControllerGripUp(XRController controller, XRRayInteractor interactor)
    {
        if (controller.controllerNode == XRNode.LeftHand)
        {
            return;
        }

        resizePoints.Remove(controller.controllerNode);

    }

    private void ControllerGripDown(XRController controller, XRRayInteractor interactor)
    {
        if (interactor.GetCurrentRaycastHit(out var hit))
        {

            if (hit.transform.TryGetComponent<Destroy>(out var _))
            {

                resizePoints[controller.controllerNode] = (hit.transform, hit.point);
                if (resizePoints.Count == 2)
                {
                    resizeStartLength = Vector3.Distance(resizePoints[XRNode.LeftHand].point, resizePoints[XRNode.RightHand].point);
                }
            }
        }
    }


    private void ControllerGripHold(XRController controller, XRRayInteractor interactor)
    {

        if (controller.controllerNode == XRNode.LeftHand)
        {
            return;
        }

        if (interactor.GetCurrentRaycastHit(out var hit) && resizePoints.ContainsKey(controller.controllerNode))
        {

            resizePoints[controller.controllerNode] = (hit.transform, hit.point);

            if (resizePoints.Count == 2)
            {
                var LeftHand = resizePoints[XRNode.LeftHand];
                var RightHand = resizePoints[XRNode.RightHand];

                if (LeftHand.target == RightHand.target)
                {

                    float sizeChange = Vector3.Distance(LeftHand.point, RightHand.point) - resizeStartLength;
                    //sizeChange = Mathf.Clamp(sizeMultiplier, 0.2f, 5f);

                    var direction = LeftHand.point - RightHand.point;
                    var dot = Vector3.Dot(transform.up, direction.normalized);

                    if (dot < 0.33f && dot > -0.33f)
                    {
                        Vector3 origScale = hit.transform.localScale;
                        origScale.x = origScale.x + sizeChange;
                        origScale.x = Mathf.Clamp(origScale.x, 0.2f, 5f);
                        hit.transform.localScale = origScale;
                    }

                    else if (dot > 0.66f || dot < -0.66f)
                    {
                        Vector3 origScale = hit.transform.localScale;
                        origScale.y = origScale.y + sizeChange;
                        origScale.y = Mathf.Clamp(origScale.y, 0.2f, 5f);
                        hit.transform.localScale = origScale;
                    }
                    else
                    {
                        Vector3 origScale = hit.transform.localScale;

                        origScale.x = origScale.x + sizeChange;
                        origScale.y = origScale.y + sizeChange;

                        origScale.x = Mathf.Clamp(origScale.x, 0.2f, 5f);
                        origScale.y = Mathf.Clamp(origScale.y, 0.2f, 5f);

                        hit.transform.localScale = origScale;
                    }

                }
            }
            resizeStartLength = Vector3.Distance(resizePoints[XRNode.LeftHand].point, resizePoints[XRNode.RightHand].point);

        }
    }



    private void AddDisplay(XRController controller, XRRayInteractor interactor)
    {
        Destroy current = Instantiate(display, controller.transform.position + controller.transform.forward * 2, controller.transform.rotation);

        if(interactor.GetCurrentRaycastHit(out var hit))
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
        if(interactor.GetCurrentRaycastHit(out var hit))
        {
            if(hit.collider.TryGetComponent(out Destroy destroy))
            {
                destroy.DestroyDisplay();
            }
        }
    }
}