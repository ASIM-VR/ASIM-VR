using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
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

    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.GetContact(0);
        transform.position = contact.point + (contact.normal * Mathf.Max(Extents.z, 0.01f));
        transform.rotation = Quaternion.LookRotation(-contact.normal, Vector3.up);
        Debug.Log("On Collision");
    }

    public Collider Collider { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public bool IsValid { get; private set; }
    public Vector3 Extents { get; private set; }
}