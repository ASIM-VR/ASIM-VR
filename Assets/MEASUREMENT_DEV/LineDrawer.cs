using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{   

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log("LineDrawer: Staring lineDrawer");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;    
    }

    public void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("LineDrawer: Drawing line");
        lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y, startPos.z));
        lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y, endPos.z));
    }

    public void ResetLine()
    {
        Debug.Log("LineDrawer: Resetting line");
        lineRenderer.SetPosition(0, new Vector3());
        lineRenderer.SetPosition(1, new Vector3());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
