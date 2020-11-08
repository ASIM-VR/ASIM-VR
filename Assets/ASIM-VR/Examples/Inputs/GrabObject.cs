using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    /// <summary>
    /// Simple object grabbing with AsimInput.
    /// </summary>
    public class GrabObject : MonoBehaviour
    {
        private int m_owner = -1;

        private void OnEnable()
        {
            //Add listener for primary trigger down and up states.
            //Primary trigger of a controller is defined by the current AsimInput
            //input implementation.
            Input.AddListener(AsimTrigger.Primary, AsimState.Down, TryStartGrab);
            Input.AddListener(AsimTrigger.Primary, AsimState.Up, TryStopGrab);
        }

        private void OnDisable()
        {
            Input.RemoveListener(AsimTrigger.Primary, AsimState.Down, TryStartGrab);
            Input.RemoveListener(AsimTrigger.Primary, AsimState.Up, TryStopGrab);
        }

        private void TryStartGrab(XRNode node, XRRayInteractor interactor)
        {
            //After the target trigger is pressed by a controller we must make sure that:
            // - the object is not being grabbed
            // - the user is pointing at the object
            if(m_owner == -1 && interactor.GetCurrentRaycastHit(out var hit) && hit.collider.gameObject == gameObject)
            {
                m_owner = (int)node;
                transform.parent = interactor.transform;
            }
        }

        private void TryStopGrab(XRNode node, XRRayInteractor interactor)
        {
            //If the target trigger is released we have to check it is the owner controller
            //that released the trigger.
            if(m_owner == (int)node)
            {
                transform.parent = null;
                m_owner = -1;
            }
        }

        private AsimInput Input => AsimInput.Instance;
    }
}