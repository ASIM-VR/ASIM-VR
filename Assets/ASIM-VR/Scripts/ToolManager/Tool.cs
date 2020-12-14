using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public abstract AsimTool Type { get; }
    public abstract string ToolName { get; }
}