using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AsimTool
{
    //Lisää tähän kaikki työkalut
    None,
    ToolPlaceholder,
    AddRemove,
    ObjectSize,
    TapeMeasure
}


public class ToolManager : MonoBehaviour
{
    private List<Tool> Tools;
    private Tool ActiveTool;

    void Awake()
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

    private void ActivateTool(AsimTool tool) {
        
        

        foreach (Tool nTool in Tools)
        {
            if (nTool.Type.Equals(tool))
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
        foreach (Tool nTool in Tools)
        {
            nTool.gameObject.SetActive(false);
        }
        ActiveTool = null;
        
        InfoDisplay.Instance.ClearText();
    }
    
    void Update()
    {
        // Temporary test functionality
        if (Input.GetKeyDown("1"))
        {
            ActivateAddRemove();
        }
        else if (Input.GetKeyDown("2"))
        {
            ActivateObjectSize();
        }
        else if (Input.GetKeyDown("3"))
        {
            ActivateTapeMeasure();
        }
        else if (Input.GetKeyDown("0"))
        {
            DeactivateTools();
        }
    }

    public string GetActiveToolName()
    {
        if (ActiveTool == null)
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
}
