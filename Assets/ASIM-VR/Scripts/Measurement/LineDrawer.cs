using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        LineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y, startPos.z));
        LineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y, endPos.z));
    }

    public void ResetLine()
    {
        LineRenderer.SetPosition(0, new Vector3());
        LineRenderer.SetPosition(1, new Vector3());
    }

    private LineRenderer GetLineRenderer()
    {
        var line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        return line;
    }

    private LineRenderer LineRenderer => lineRenderer ?? (lineRenderer = GetLineRenderer());
}