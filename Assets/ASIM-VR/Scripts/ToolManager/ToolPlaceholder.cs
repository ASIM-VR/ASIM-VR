using UnityEngine;

// Esimerkki, miten rekisteröidytään työkaluksi
public class ToolPlaceholder : Tool
{
    public override AsimTool Type => AsimTool.ToolPlaceholder;
    public override string ToolName => "Tool placeholder";

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            Debug.Log("tool placeholder: k painettu");
        }
    }
}