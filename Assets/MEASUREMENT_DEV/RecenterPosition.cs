using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.LegacyInputHelpers;


// https://docs.unity3d.com/2019.4/Documentation/Manual/xr_input.html
public class RecenterPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.XR.InputTracking.Recenter();
        //XRInputSubsystem.TryRecenter();
    }
}
