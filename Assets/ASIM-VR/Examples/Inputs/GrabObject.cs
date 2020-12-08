using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    public class GrabTarget
    {
        private readonly bool m_wasKinematic;
        private readonly Transform m_parent;

        public GrabTarget()
        {
            IsValid = false;
        }

        public GrabTarget(Rigidbody rigidbody, Collider collider)
        {
            Rigidbody = rigidbody;
            Collider = collider;
            IsValid = rigidbody != null && collider != null;
            m_parent = Rigidbody.transform.parent;
            m_wasKinematic = Rigidbody.isKinematic;
        }

        public void StartGrab(Transform parent)
        {
            if(IsValid)
            {
                transform.parent = parent;
                Extents = Collider.bounds.extents;
                Collider.enabled = false;
                Rigidbody.isKinematic = true;
            }
        }

        public void StopGrab()
        {
            if(IsValid)
            {
                Collider.enabled = true;
                Rigidbody.isKinematic = m_wasKinematic;
                transform.parent = m_parent;
            }
        }

        public Transform transform => Rigidbody.transform;

        public Collider Collider { get; }
        public Rigidbody Rigidbody { get; }
        public bool IsValid { get; }
        public Vector3 Extents { get; private set; }
    }

    /// <summary>
    /// Simple object grabbing with AsimInput.
    /// </summary>
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

        [SerializeField]
        private XRNode m_hand;

        private GrabTarget m_target;

        private void Awake()
        {
            m_target = new GrabTarget();
        }

        private void OnEnable()
        {
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

        private void Reset()
        {
            if(TryGetComponent(out XRController controller))
            {
                m_hand = controller.controllerNode;
            }
        }

        private void TryStartGrab(XRController controller, XRRayInteractor interactor)
        {
            if(IsOwner(controller) && TryGetTarget(interactor, out m_target))
            {
                m_target.StartGrab(transform);
                if(m_style == GrabStyle.Grab)
                {
                    m_target.transform.localPosition = Vector3.zero;
                }
            }
        }

        private void TryMoveGrab(XRController controller, XRRayInteractor interactor)
        {
            if(m_target.IsValid && IsOwner(controller) && m_style == GrabStyle.Laser)
            {
                TryMoveTo(interactor);
            }
        }

        private void TryStopGrab(XRController controller, XRRayInteractor interactor)
        {
            if(IsOwner(controller))
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
                if(hit.transform.TryGetComponent(out Rigidbody rigidbody))
                {
                    target = new GrabTarget(rigidbody, hit.transform.GetComponent<Collider>());
                    return true;
                }
            }
            target = new GrabTarget();
            return false;
        }

        private bool IsOwner(XRController controller)
        {
            return m_hand == controller.controllerNode;
        }

        private AsimInput Input => AsimInput.Instance;
    }
}