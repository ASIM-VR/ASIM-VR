using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esimerkki, miten rekisteröidytään työkaluksi
public class ToolPlaceholder : Tool
{
    public override AsimTool Type => AsimTool.ToolPlaceholder;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("tool placeholder: k painettu");
        }
    }
}
