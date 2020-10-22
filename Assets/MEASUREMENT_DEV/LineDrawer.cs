using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{   

    [SerializeField]
    private LineRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {   
        renderer = GetComponent<LineRenderer>();
        renderer.positionCount = 2;    
    }


    public void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        renderer.SetPosition(0, new Vector3(startPos.x, startPos.y, startPos.z));
        renderer.SetPosition(1, new Vector3(endPos.x, endPos.y, endPos.z));
    }


    public void ResetLine()
    {
        renderer.SetPosition(0, new Vector3());
        renderer.SetPosition(1, new Vector3());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
