//using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class DisplayAdd : MonoBehaviour
{
    
    [SerializeField]
    private Destroy display;
    
    Camera mainCamera;

    [SerializeField]
    private XRRayInteractor controllerRay;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Instantiate(display, mainCamera.transform.position + mainCamera.transform.forward * 2, mainCamera.transform.rotation);
        }

        if ( Input.GetKeyDown("r") ) {
            
            RaycastHit hit;
            if(controllerRay.GetCurrentRaycastHit(out hit))
            {
                Destroy disp = hit.collider.GetComponent<Destroy>();

                if (disp != null)
                {
                    disp.DestroyDisplay();
                }
            }
        }

    }
}
