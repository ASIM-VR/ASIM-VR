using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class GrabTarget : MonoBehaviour
{
    [SerializeField]
    private bool grabParent;

    private bool m_wasKinematic;
    private Transform m_parent;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        IsValid = Rigidbody != null && Collider != null;
        m_wasKinematic = Rigidbody.isKinematic;
        m_parent = Transform.parent;
    }

    public void StartGrab(Transform parent)
    {
        if(IsValid)
        {
            //TODO: Cache local space bounds.extents to Extents.
            //      Using Collider.bounds returns the global space bounds,
            //      which are affected by the objects rotation causing
            //      incorrect offset.
            Transform.parent = parent;
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
            Transform.parent = m_parent;
        }
    }

    public Transform Transform => grabParent ? transform.parent : transform;

    public Collider Collider { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public bool IsValid { get; private set; }
    public Vector3 Extents { get; private set; }
}