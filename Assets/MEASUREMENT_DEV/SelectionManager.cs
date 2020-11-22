using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionManager : MonoBehaviour {

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private XRRayInteractor controller;
    private Transform _selection;
    private Renderer selectionRenderer;
    [SerializeField] private float grabDistance = 1;
    [SerializeField] private float grabDistanceChangeSpeed= 0.01f;
    [SerializeField] private float maxGrabDistance = 5;
    
    private void applyMaterial(bool useDefaultMaterial = false) {
        if (useDefaultMaterial){
            selectionRenderer = _selection.GetComponent<Renderer> ();
            selectionRenderer.material = highlightMaterial;
        } else {
            selectionRenderer = _selection.GetComponent<Renderer> ();
            selectionRenderer.material = defaultMaterial;
        }
    }

    private void Start () {
        UpdateSelectionPosition();
    }


    private void UpdateSelectionPosition() {
        if (_selection != null)
        {
            _selection.transform.position = controller.transform.forward * grabDistance;
            _selection.transform.rotation = controller.transform.rotation;
        }
    }

    private void AdjustGrabDistance (){
        if (Input.mouseScrollDelta.y > 0)
        {
            var newPos =  _selection.transform.forward * grabDistanceChangeSpeed;
            if(Vector3.Distance(_selection.position, newPos) < maxGrabDistance)
            {
                _selection.position += newPos;
            }

        } else if (Input.mouseScrollDelta.y < 0)
        {
            var newPos =  _selection.transform.forward * grabDistanceChangeSpeed;

            if ((_selection.position - _selection.transform.forward * grabDistanceChangeSpeed).z > 1){
                _selection.position -= _selection.transform.forward * grabDistanceChangeSpeed;
            }
        }

    }


    private void HandleGrabbing(){
        controller.GetCurrentRaycastHit (out var ray);

        if (Input.GetKeyDown ("f")) {

            if (_selection != null) {

                _selection.GetComponent<BoxCollider> ().enabled = true;
                _selection.GetComponent<Rigidbody> ().useGravity = true;
                applyMaterial();
                _selection.transform.parent = null;
                _selection = null;
                return;
            }

            _selection = ray.transform;
            if (_selection != null && _selection.CompareTag (selectableTag)) {

                _selection.GetComponent<BoxCollider> ().enabled = false;
                _selection.GetComponent<Rigidbody> ().useGravity = false;

                UpdateSelectionPosition();
                _selection.transform.parent = GameObject.Find ("RightHand Controller").transform;
                applyMaterial(true);
            }
        }
    }



    private void Update () {
        HandleGrabbing();        
        AdjustGrabDistance();
    }   
    
}