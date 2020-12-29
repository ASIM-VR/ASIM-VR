using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Transform))]

public class GrabTarget : MonoBehaviour
{
    private bool m_wasKinematic;
    private Transform m_parent;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        IsValid = Rigidbody != null && Collider != null;
        m_wasKinematic = Rigidbody.isKinematic;
        m_parent = transform.parent;
    }

    public void StartGrab(Transform parent)
    {
        if(IsValid)
        {
            //TODO: Cache local space bounds.extents to Extents.
            //      Using Collider.bounds returns the global space bounds,
            //      which are affected by the objects rotation causing
            //      incorrect offset.
            transform.parent = parent;
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

    // void OnCollisionEnter(Collision collision)
    // {
    //     var contact = collision.GetContact(0);
    //     this.transform.position = contact.point + (contact.normal * Mathf.Max(this.Extents.z, 0.01f));
    //     this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, -contact.normal);
    //     Debug.Log("On Collision");
    // }

    private void Update() {
        
        var hits = Physics.RaycastAll(transform.position, Vector3.up, 1f);

        foreach(var hit in hits)
        {
            if (hit.collider.gameObject == transform.gameObject)
            {
                continue;
            }

        transform.position = hit.point + (hit.normal * Mathf.Max(Extents.z, 0.01f));
        // transform.rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            break;
        }

        // transform.localPosition = Vector3.forward;
        // transform.rotation = interactor.transform.rotation;

    }

    public Collider Collider { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public bool IsValid { get; private set; }
    public Vector3 Extents { get; private set; }
}