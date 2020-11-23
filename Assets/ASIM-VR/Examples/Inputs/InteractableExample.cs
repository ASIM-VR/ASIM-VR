using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class InteractableExample : MonoBehaviour
    {
        private XRSimpleInteractable m_target;

        private void OnEnable()
        {
            m_target = GetComponent<XRSimpleInteractable>();
            m_target.onSelectEnter.AddListener(HelloWorld);
        }

        private void OnDisable()
        {
            m_target.onSelectEnter.RemoveListener(HelloWorld);
        }

        private void HelloWorld(XRBaseInteractor interactor)
        {
            Debug.Log("Hello World!");
        }
    }
}