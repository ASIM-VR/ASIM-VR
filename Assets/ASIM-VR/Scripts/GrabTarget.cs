using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class GrabTarget : MonoBehaviour
{
    private bool m_wasKinematic;
    private Transform m_parent;
    private Rigidbody rigidbody;
    private Collider collider;
    private void Awake() {
        rigidbody = GetComponent<Rigidbody>(); 
        collider= GetComponent<Collider>();
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

    void OnCollisionEnter(Collision collision)
    {
        var contact = collision.GetContact(0);
        this.transform.position = contact.point + (contact.normal * Mathf.Max(this.Extents.z, 0.01f));
        this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, -contact.normal);
        Debug.Log("On Collision");
    }

    public Transform transform => Rigidbody.transform;

    public Collider Collider { get;set; }
    public Rigidbody Rigidbody { get;set; }
    public bool IsValid { get; set;}
    public Vector3 Extents { get; private set; }
}
