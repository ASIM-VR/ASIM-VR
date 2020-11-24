using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoDisplay : MonoBehaviour
{
    private TextMeshProUGUI textField;
    private static InfoDisplay _Instance;
    // Start is called before the first frame update

    public static InfoDisplay Instance
    {
        get
        {
            return _Instance;
        }
        set
        {
            if (value != null)
            {
                _Instance = value;
            }
        }
    }

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
