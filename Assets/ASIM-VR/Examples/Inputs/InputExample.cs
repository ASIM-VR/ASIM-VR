using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    public class InputExample : MonoBehaviour
    {
        private void OnEnable()
        {
            AsimInput.Instance.AddListener(AsimTrigger.Primary, AsimState.Down, HelloWorld);
        }

        private void OnDisable()
        {
            AsimInput.Instance.RemoveListener(AsimTrigger.Primary, AsimState.Down, HelloWorld);
        }

        private void HelloWorld(XRNode node, XRRayInteractor interactor)
        {
            Debug.Log("Hello World!");
        }
    }
}