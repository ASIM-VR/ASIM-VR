using AsimVr.Inputs.Core;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Use <see cref="XRController"/> test methods to simulate XR input.
    /// </summary>
    [MockHMDOnly]
    [DefaultExecutionOrder(100)]
    public class MouseXRController : MonoBehaviour
    {
        [SerializeField]
        private XRController m_rightHandController;

        [SerializeField]
        private XRController m_leftHandController;

        private XRControllerBinding m_rightHand;
        private XRControllerBinding m_leftHand;

        private void Awake()
        {
            m_rightHand = new XRControllerBinding(m_rightHandController);
            m_leftHand = new XRControllerBinding(m_leftHandController);
        }

        private void Reset()
        {
            //Try to find the current left and right hand controllers.
            foreach(var controller in FindObjectsOfType<XRController>())
            {
                if(controller.controllerNode == XRNode.RightHand)
                {
                    m_rightHandController = controller;
                }

                if(controller.controllerNode == XRNode.LeftHand)
                {
                    m_leftHandController = controller;
                }
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 20), $"Using {(UseLeft ? "left" : "right")} hand controller");
        }

        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Current.Select();
                Current.UIPress();
            }

            if(Input.GetMouseButton(1))
            {
                Current.Active();
            }
        }

        private XRControllerBinding Current => UseLeft ? m_leftHand : m_rightHand;
        private bool UseLeft => Input.GetKey(KeyCode.LeftShift);
    }
}