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
            nTool.gameObject.SetActive(nTool.Type.Equals(tool));
        }
    }
    private void DeactivateTools()
    {
        foreach (Tool nTool in Tools)
        {
            nTool.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        // Temporary test functionality
        if (Input.GetKeyDown("1"))
        {
            ActivateAddRemove();
        }
        else if(Input.GetKeyDown("2"))
        {
            ActivateObjectSize();
        }
        else if (Input.GetKeyDown("0"))
        {
            DeactivateTools();
        }
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
