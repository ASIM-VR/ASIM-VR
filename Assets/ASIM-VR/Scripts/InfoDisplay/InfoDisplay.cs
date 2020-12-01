using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoDisplay : MonoBehaviour
{
    private TextMeshProUGUI textField;

    public static InfoDisplay Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("InfoDisplay instance already set!", Instance);
            return;
        }

        Instance = this;
        textField = GetComponentInChildren<TextMeshProUGUI>();
        ClearText();
    }

    public void SetText(params string[] strArray)
    {
        textField.SetText(string.Join("\r\n", strArray));
    }


    public void ClearText()
    {
        textField.SetText("");
    }

}
