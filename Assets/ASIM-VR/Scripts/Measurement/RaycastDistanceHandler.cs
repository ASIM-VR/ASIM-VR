using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastDistanceHandler : Tool
{
    private enum MeasureState
    {
        NoPoints,
        OnePoint,
        TwoPoints,
    }

    public override AsimTool Type => AsimTool.TapeMeasure;
    public override string ToolName => "Object distance calculator";

    [SerializeField]
    private XRRayInteractor controllerRaycast;

    [SerializeField]
    private LineDrawer lineDrawer;

    [SerializeField]
    private XRNode hand = XRNode.RightHand;

    private MeasureState measureState;
    private Vector3 point1;
    private Vector3 point2;

    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, SetPoint);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, SetPoint);
    }

    private void Reset()
    {
        foreach(var controller in FindObjectsOfType<XRController>())
        {
            if(controller.controllerNode == hand && controller.TryGetComponent(out XRRayInteractor interactor))
            {
                controllerRaycast = interactor;
            }
        }
        lineDrawer = GetComponentInChildren<LineDrawer>();
    }

    private void Update()
    {
        RenderText();
    }

    private void SetPoint(XRController controller, XRRayInteractor interactor)
    {
        if(controller.controllerNode == hand)
        {
            MeasureDistance(interactor);
        }
    }

    private void MeasureDistance(XRRayInteractor interactor)
    {
        if(measureState == MeasureState.TwoPoints)
        {
            ResetPoints();
        }
        else if(interactor.GetCurrentRaycastHit(out RaycastHit rayhit))
        {
            if(measureState == MeasureState.NoPoints)
            {
                point1 = rayhit.point;
                measureState = MeasureState.OnePoint;
            }
            else if(measureState == MeasureState.OnePoint)
            {
                point2 = rayhit.point;
                lineDrawer.DrawLine(point1, point2);
                measureState = MeasureState.TwoPoints;
            }
        }
    }

    private void ResetPoints()
    {
        lineDrawer.ResetLine();
        lineDrawer.enabled = false;
        measureState = MeasureState.NoPoints;
    }

    private string GetDistanceText()
    {
        return controllerRaycast.GetCurrentRaycastHit(out var hit)
            ? $"{hit.distance:0.00}m"
            : "No target";
    }

    private string GetPointDistanceText()
    {
        var distance = Vector3.Distance(point1, point2);
        return $"Distance: {distance:0.00}m";
    }

    private void RenderText()
    {
        switch(measureState)
        {
            case MeasureState.OnePoint:
                InfoDisplay.Instance.SetText(
                    GetDistanceText(),
                    $"Point 1: {point1}");
                break;

            case MeasureState.TwoPoints:
                InfoDisplay.Instance.SetText(
                    GetDistanceText(),
                    $"Point 1: {point1}",
                    $"Point 2: {point2}",
                    GetPointDistanceText());
                break;

            default:
                InfoDisplay.Instance.SetText(GetDistanceText());
                break;
        }
    }
}