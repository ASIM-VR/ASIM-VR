using System;
using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Simple object grabbing with AsimInput.
/// </summary>
public class GrabManager : MonoBehaviour
{
    public enum GrabStyle
    {
        Grab,
        GrabOffset,
        Laser
    }

    [SerializeField]
    private GrabStyle m_style;

    [SerializeField]
    private XRNode m_hand;

    [SerializeField]
    private float grabZoomSpeed = 0.1f;

    [SerializeField]
    private float maxZoomDistance = 5f;

    [SerializeField]
    private float minZoomDistance = 0.5f;

    private GrabTarget m_target;

    private void Awake()
    {
        m_target = null;
    }

    

    private void OnEnable()
    {
        Input.AddListener(InputHelpers.Button.Grip, AsimState.Down, TryStartGrab);
        Input.AddListener(InputHelpers.Button.Grip, AsimState.Hold, TryMoveGrab);
        Input.AddListener(InputHelpers.Button.Grip, AsimState.Up, TryStopGrab);
        Input.AddListener(InputHelpers.Button.PrimaryAxis2DUp, AsimState.Hold, MoveGrabbedItemForward);
        Input.AddListener(InputHelpers.Button.PrimaryAxis2DDown, AsimState.Hold, MoveGrabbedItemBack);
        Input.AddListener(InputHelpers.Button.Primary2DAxisClick, AsimState.Down, ChangeGrabStyle);
    }

    private void OnDisable()
    {
        Input.RemoveListener(InputHelpers.Button.Grip, AsimState.Down, TryStartGrab);
        Input.RemoveListener(InputHelpers.Button.Grip, AsimState.Hold, TryMoveGrab);
        Input.RemoveListener(InputHelpers.Button.Grip, AsimState.Up, TryStopGrab);
        Input.RemoveListener(InputHelpers.Button.PrimaryAxis2DUp, AsimState.Hold, MoveGrabbedItemForward);
        Input.RemoveListener(InputHelpers.Button.PrimaryAxis2DDown, AsimState.Hold, MoveGrabbedItemBack);
        Input.RemoveListener(InputHelpers.Button.Primary2DAxisClick, AsimState.Down, ChangeGrabStyle);
    }


    private void ChangeGrabStyle(XRController controller, XRRayInteractor interactor)
    {
        int currentIndex = (int) m_style;
        m_style = GetNextStyle(currentIndex);
    }

    private GrabStyle GetNextStyle(int currentIndex)
    {

        var enums = (GrabStyle[])Enum.GetValues(typeof(GrabStyle));
        return currentIndex + 1 < enums.Length? enums[currentIndex+1] : enums[0]; 
    }

    private void Reset()
    {
        if(TryGetComponent(out XRController controller))
        {
            m_hand = controller.controllerNode;
        }
    }

    private void MoveGrabbedItemForward(XRController controller, XRRayInteractor interactor)
    {
        if(IsOwner(controller) && m_target != null)
        {
            if(Vector3.Distance(interactor.attachTransform.transform.position, m_target.transform.position) <= maxZoomDistance)
            {
                m_target.transform.rotation = interactor.attachTransform.rotation;
                m_target.transform.position += m_target.transform.forward * grabZoomSpeed * Time.deltaTime;
            }
        }
    }

    private void MoveGrabbedItemBack(XRController controller, XRRayInteractor interactor)
    {
        if(IsOwner(controller) && m_target != null)
        {
            if(Vector3.Distance(interactor.attachTransform.transform.position, m_target.transform.position) >= minZoomDistance)
            {
                m_target.transform.rotation = interactor.attachTransform.rotation;
                m_target.transform.position -= m_target.transform.forward * grabZoomSpeed * Time.deltaTime;
            }
        }
    }

    private void TryStartGrab(XRController controller, XRRayInteractor interactor)
    {
        if(IsOwner(controller) && TryGetTarget(interactor, out m_target))
        {
            if (m_target != null){
                m_target.StartGrab(transform);
                if(m_style == GrabStyle.Grab)
                {
                    m_target.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    private void TryMoveGrab(XRController controller, XRRayInteractor interactor)
    {

        if(m_target != null && m_target.IsValid && IsOwner(controller) && m_style == GrabStyle.Laser)
        {
            TryMoveTo(interactor);
        }
    }

    private void TryStopGrab(XRController controller, XRRayInteractor interactor)
    {
        if(m_target != null && IsOwner(controller))
        {
            m_target.StopGrab();
        }
    }

    private void TryMoveTo(XRRayInteractor interactor)
    {
        if(interactor.GetCurrentRaycastHit(out var hit))
        {
            //Move to the current target position and offset from the surface based on the current normal.
            m_target.transform.position = hit.point + (hit.normal * Mathf.Max(m_target.Extents.z, 0.01f));
            //Align with the current surface.
            m_target.transform.rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            return;
        }
        m_target.transform.localPosition = Vector3.forward;
        m_target.transform.rotation = interactor.transform.rotation;
    }

    private bool TryGetTarget(XRRayInteractor interactor, out GrabTarget target)
    {
        if(interactor.GetCurrentRaycastHit(out var hit))
        {
            if(hit.transform.TryGetComponent(out GrabTarget grabTarget))
            {
                target = grabTarget;
                return true;
            }
        }
        target = null;
        return false;
    }

    private bool IsOwner(XRController controller)
    {
        return m_hand == controller.controllerNode;
    }

    private AsimInput Input => AsimInput.Instance;
}