using AsimVr.Inputs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using ZenFulcrum.EmbeddedBrowser;

public class DisplayAdd : Tool
{
    public override AsimTool Type => AsimTool.AddRemove;
    public override string ToolName => "Add or remove displays";

    private readonly float minScale = 0.2f;
    private readonly float maxScale = 5f;

    [SerializeField]
    private Destroy display;

    private Dictionary<XRNode, (Transform target, Vector3 point)> resizePoints;
    private float previousLength;

    private Browser browser;


    private void Awake()
    {
        resizePoints = new Dictionary<XRNode, (Transform target, Vector3 point)>();
    }

    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.PrimaryButton, AsimState.Down, AddDisplay);
        AsimInput.Instance.AddListener(InputHelpers.Button.SecondaryButton, AsimState.Down, SecondaryButtonClick);
        AsimInput.Instance.AddListener(InputHelpers.Button.Grip, AsimState.Down, ControllerGripDown);
        AsimInput.Instance.AddListener(InputHelpers.Button.Grip, AsimState.Hold, ControllerGripHold);
        AsimInput.Instance.AddListener(InputHelpers.Button.Grip, AsimState.Up, ControllerGripUp);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(InputHelpers.Button.PrimaryButton, AsimState.Down, AddDisplay);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.SecondaryButton, AsimState.Down, SecondaryButtonClick);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Grip, AsimState.Down, ControllerGripDown);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Grip, AsimState.Hold, ControllerGripHold);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Grip, AsimState.Up, ControllerGripUp);
    }

    private void ControllerGripUp(XRController controller, XRRayInteractor interactor)
    {
        if(XRSettings.loadedDeviceName == "MockHMD" && controller.controllerNode == XRNode.LeftHand)
        {
            return;
        }

        resizePoints.Remove(controller.controllerNode);
    }

    private void ControllerGripDown(XRController controller, XRRayInteractor interactor)
    {
        if(interactor.GetCurrentRaycastHit(out var hit))
        {
            if(hit.transform.TryGetComponent<Destroy>(out var _))
            {
                resizePoints[controller.controllerNode] = (hit.transform, hit.point);
                if(resizePoints.Count == 2)
                {
                    //Set the inital length between the right and left hand points.
                    previousLength = Vector3.Distance(resizePoints[XRNode.LeftHand].point, resizePoints[XRNode.RightHand].point);
                }
            }
        }
    }

    private void ControllerGripHold(XRController controller, XRRayInteractor interactor)
    {
        if(XRSettings.loadedDeviceName == "MockHMD" && controller.controllerNode == XRNode.LeftHand)
        {
            return;
        }

        if(interactor.GetCurrentRaycastHit(out var hit) && resizePoints.ContainsKey(controller.controllerNode))
        {
            resizePoints[controller.controllerNode] = (hit.transform, hit.point);

            if(resizePoints.Count == 2)
            {
                var leftHand = resizePoints[XRNode.LeftHand];
                var rightHand = resizePoints[XRNode.RightHand];

                if(leftHand.target == rightHand.target)
                {
                    var sizeChange = Vector3.Distance(leftHand.point, rightHand.point) - previousLength;
                    var direction = leftHand.point - rightHand.point;
                    var dot = Mathf.Abs(Vector3.Dot(transform.up, direction.normalized));
                    var scale = hit.transform.localScale;

                    if(dot < 0.33f)
                    {
                        scale.x = Mathf.Clamp(scale.x + sizeChange, minScale, maxScale);
                    }
                    else if(dot > 0.66f)
                    {
                        scale.y = Mathf.Clamp(scale.y + sizeChange, minScale, maxScale);
                    }
                    else
                    {
                        scale.x = Mathf.Clamp(scale.x + sizeChange, minScale, maxScale);
                        scale.y = Mathf.Clamp(scale.y + sizeChange, minScale, maxScale);
                    }

                    hit.transform.localScale = scale;
                }
                //Update the length between the right and left hand points.
                previousLength = Vector3.Distance(leftHand.point, rightHand.point);
            }
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
            current.transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
        }
    }




    private void SecondaryButtonClick(XRController controller, XRRayInteractor interactor)
    {
        if(controller.controllerNode == XRNode.LeftHand)
        {
            FindAndRemove(controller, interactor);
        }
        else
        {
            FindAndSelect(controller, interactor);
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


    private void FindAndSelect(XRController controller, XRRayInteractor interactor)
    {
        if (interactor.GetCurrentRaycastHit(out var hit))
        {
            if (hit.collider.TryGetComponent(out Browser b))
            {
                browser = b;
                Debug.Log(browser); 
            }
        }
    }

}