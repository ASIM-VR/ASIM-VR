using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    public class InputExample : MonoBehaviour
    {
        private void OnEnable()
        {
            AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, HelloWorld);
        }

        private void OnDisable()
        {
            AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, HelloWorld);
        }

        private void HelloWorld(XRController controller, XRRayInteractor interactor)
        {
            Debug.Log("Hello World!");
        }
    }
}