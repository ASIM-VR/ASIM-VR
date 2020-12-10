using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class InfoDisplay : MonoBehaviour
{
    private TextMeshProUGUI textField;
    private ToolManager toolManager;

    public static InfoDisplay Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("InfoDisplay instance already set!", Instance);
            return;
        }
        Instance = this;
        textField = GetComponentInChildren<TextMeshProUGUI>();

        toolManager = FindObjectOfType<ToolManager>();

        ClearText();
    }

    public void SetText(params string[] strArray)
    {
        string activeToolName = toolManager.GetActiveToolName();

        if(activeToolName != null)
        {
            textField.SetText(activeToolName + "\r\n" + string.Join("\r\n", strArray));
        }
        else
        {
            textField.SetText(string.Join("\r\n", strArray));
        }
    }

    public void ClearText()
    {
        string activeToolName = toolManager.GetActiveToolName();

        if(activeToolName != null)
        {
            textField.SetText(activeToolName + "\r\n");
        }
        else
        {
            textField.SetText("");
        }
    }
}