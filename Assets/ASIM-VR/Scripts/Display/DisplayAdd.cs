using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ZenFulcrum.EmbeddedBrowser;

public class DisplayAdd : Tool
{
    public override AsimTool Type => AsimTool.AddRemove;
    public override string ToolName => "Add or remove displays";

    [SerializeField]
    private ExternalKeyboard keyboard;

    [SerializeField]
    private VRBrowserPanel display;

    private VRBrowserPanel browserPanel;

    private void Awake()
    {
        keyboard.onFocusChange += OnFocusChange;
    }

    private void OnFocusChange(Browser browser, bool editable)
    {
        if(browserPanel != null)
        {
            keyboard.transform.position = browserPanel.keyboardLocation.transform.position;
            keyboard.transform.rotation = browserPanel.keyboardLocation.transform.rotation;
        }
    }

    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.SecondaryButton, AsimState.Down, SecondaryButtonClick);
    }

    private void OnDisable()
    {
        DeSelectDisplay();
        AsimInput.Instance.RemoveListener(InputHelpers.Button.SecondaryButton, AsimState.Down, SecondaryButtonClick);
    }

    private void AddDisplay(XRController controller, XRRayInteractor interactor)
    {
        var current = Instantiate(display, controller.transform.position + controller.transform.forward * 2, controller.transform.rotation);
        SelectDisplay(current);
        if(interactor.GetCurrentRaycastHit(out var hit))
        {
            current.transform.position = hit.point + (hit.normal * 0.01f);
            current.transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
        }
    }

    private void SecondaryButtonClick(XRController controller, XRRayInteractor interactor)
    {
        if(interactor.GetCurrentRaycastHit(out var hit) &&
            hit.transform.parent != null &&
            hit.transform.parent.TryGetComponent(out VRBrowserPanel panel))
        {
            SelectDisplay(panel);
        }
        else
        {
            AddDisplay(controller, interactor);
        }
    }

    private void SelectDisplay(VRBrowserPanel panel)
    {
        DeSelectDisplay();
        browserPanel = panel;
        browserPanel.controlBrowser.gameObject.SetActive(true);
    }

    private void DeSelectDisplay()
    {
        if(browserPanel != null)
        {
            browserPanel.controlBrowser.gameObject.SetActive(false);
        }
    }
}