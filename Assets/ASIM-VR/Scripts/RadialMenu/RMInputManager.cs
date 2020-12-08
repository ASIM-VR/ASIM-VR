using AsimVr.Inputs;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RMInputManager : MonoBehaviour
{
    [Header("Scene objects")]
    public RadialMenu RadialMenu = null;

    [SerializeField]
    private XRNode hand = XRNode.LeftHand;

    private void OnEnable()
    {
        AsimInput.Instance.AddListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Down, ShowRadialMenu);
        AsimInput.Instance.AddListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Up, HideRadialMenu);
        AsimInput.Instance.AddListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Hold, UpdateTouchPosition);
        AsimInput.Instance.AddListener(InputHelpers.Button.Primary2DAxisClick, AsimState.Down, SelectActive);
    }

    private void OnDisable()
    {
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Down, ShowRadialMenu);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Up, HideRadialMenu);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Hold, UpdateTouchPosition);
        AsimInput.Instance.RemoveListener(InputHelpers.Button.Primary2DAxisClick, AsimState.Down, SelectActive);
    }

    private void ShowRadialMenu(XRController controller, XRRayInteractor interactor)
    {
        if(controller.controllerNode == hand)
        {
            RadialMenu.Show(true);
        }
    }

    private void UpdateTouchPosition(XRController controller, XRRayInteractor interactor)
    {
        if(controller.controllerNode == hand &&
           controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 touchPosition) &&
           touchPosition != Vector2.zero)
        {
            RadialMenu.SetTouchPosition(touchPosition);
            RadialMenu.ActivateHighlithedSection();
        }
    }

    private void HideRadialMenu(XRController controller, XRRayInteractor interactor)
    {
        if(controller.controllerNode == hand)
        {
            RadialMenu.Show(false);
        }
    }

    private void SelectActive(XRController controller, XRRayInteractor interactor)
    {
        if(controller.controllerNode == hand)
        {
            //RadialMenu.ActivateHighlithedSection();
        }
    }
}