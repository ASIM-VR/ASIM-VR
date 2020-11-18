using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabItem : MonoBehaviour
{
   
    public Transform dest;
    public bool grabbing = false;

    private void Update() {

        if (Input.GetKeyDown("f")){
            grabbing = !grabbing;
        }

        if (grabbing){
         GetComponent<Rigidbody>().useGravity = false;
         this.transform.position = dest.position;
         this.transform.parent = GameObject.Find("RightHand Controller").transform;
        } else {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
        }
        
     }

}
