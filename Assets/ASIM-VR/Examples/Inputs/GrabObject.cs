using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    /// <summary>
    /// Simple object grabbing with AsimInput.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class GrabObject : MonoBehaviour
    {
        public enum GrabStyle
        {
            Grab,
            GrabOffset,
            Laser
        }

        [SerializeField]
        private GrabStyle m_style;

        private int m_owner = -1;
        private Collider m_collider;

        private void Awake()
        {
            m_collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            //Add listener for primary trigger down and up states.
            //Primary trigger of a controller is defined by the current AsimInput
            //input implementation.
            Input.AddListener(InputHelpers.Button.Trigger, AsimState.Down, TryStartGrab);
            Input.AddListener(InputHelpers.Button.Trigger, AsimState.Hold, TryMoveGrab);
            Input.AddListener(InputHelpers.Button.Trigger, AsimState.Up, TryStopGrab);
        }

        private void OnDisable()
        {
            Input.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, TryStartGrab);
            Input.RemoveListener(InputHelpers.Button.Trigger, AsimState.Hold, TryMoveGrab);
            Input.RemoveListener(InputHelpers.Button.Trigger, AsimState.Up, TryStopGrab);
        }

        private void TryStartGrab(XRController controller, XRRayInteractor interactor)
        {
            //After the target trigger is pressed by a controller we must make sure that:
            // - the object is not being grabbed
            // - the user is pointing at the object
            if(m_owner == -1 && interactor.GetCurrentRaycastHit(out var hit) && hit.collider.gameObject == gameObject)
            {
                m_owner = (int)controller.controllerNode;
                transform.parent = interactor.transform;
                switch(m_style)
                {
                    case GrabStyle.Grab:
                        transform.localPosition = Vector3.zero;
                        break;

                    case GrabStyle.Laser:
                        //Disable collision so that the XR ray interactor can hit something else.
                        m_collider.enabled = false;
                        break;
                }
            }
        }

        private void TryMoveGrab(XRController controller, XRRayInteractor interactor)
        {
            if(!m_collider.enabled &&
                m_style == GrabStyle.Laser &&
                m_owner == (int)controller.controllerNode &&
                interactor.GetCurrentRaycastHit(out var hit))
            {
                //Move to the current target position and offset from the surface based on the current normal.
                transform.position = hit.point + (hit.normal * Mathf.Max(m_collider.bounds.extents.z, 0.01f));
                //Align with the current surface.
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            }
        }

        private void TryStopGrab(XRController controller, XRRayInteractor interactor)
        {
            //If the target trigger is released we have to check it is the owner controller
            //that released the trigger.
            if(m_owner == (int)controller.controllerNode)
            {
                transform.parent = null;
                m_owner = -1;
                m_collider.enabled = true;
            }
        }

        private AsimInput Input => AsimInput.Instance;
    }
}