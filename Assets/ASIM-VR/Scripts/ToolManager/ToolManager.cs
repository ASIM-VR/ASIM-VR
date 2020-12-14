using System.Collections.Generic;
using UnityEngine;

public enum AsimTool
{
    //Lisää tähän kaikki työkalut
    None,

    ToolPlaceholder,
    AddRemove,
    ObjectSize,
    TapeMeasure,
    DistanceFromGround
}

public class ToolManager : MonoBehaviour
{
    private List<Tool> Tools;
    private Tool ActiveTool;

    private void Awake()
    {
        Tools = new List<Tool>();

        Tool[] tmp_tools = FindObjectsOfType<Tool>();

        Debug.Log("tmp_tools length" + tmp_tools.Length);

        foreach(Tool tool in tmp_tools)
        {
            Tools.Add(tool);
        }

        DeactivateTools();
    }

    private void Update()
    {
        if(Input.GetKeyDown("0"))
        {
            DeactivateTools();
        }
    }

    private void ActivateTool(AsimTool tool)
    {
        foreach(Tool nTool in Tools)
        {
            if(nTool.Type.Equals(tool))
            {
                nTool.gameObject.SetActive(true);
                ActiveTool = nTool;
            }
            else
            {
                nTool.gameObject.SetActive(false);
            }
        }
        InfoDisplay.Instance.ClearText();
    }

    private void DeactivateTools()
    {
        foreach(Tool nTool in Tools)
        {
            nTool.gameObject.SetActive(false);
        }
        ActiveTool = null;

        InfoDisplay.Instance.ClearText();
    }

    public string GetActiveToolName()
    {
        if(ActiveTool == null)
        {
            return null;
        }
        return ActiveTool.ToolName;
    }

    // Tämä tarvii tehdä jokaiselle työkalulle
    public void ActivateToolPlaceholder()
    {
        ActivateTool(AsimTool.ToolPlaceholder);
    }

    public void ActivateAddRemove()
    {
        ActivateTool(AsimTool.AddRemove);
    }

    public void ActivateTapeMeasure()
    {
        ActivateTool(AsimTool.TapeMeasure);
    }

    public void ActivateObjectSize()
    {
        ActivateTool(AsimTool.ObjectSize);
    }

    public void ActivateDistanceFromGround()
    {
        ActivateTool(AsimTool.DistanceFromGround);
    }
}