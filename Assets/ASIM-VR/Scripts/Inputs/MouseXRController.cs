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
        [Header("References")]
        [SerializeField]
        private XRController m_rightHandController;

        [SerializeField]
        private XRController m_leftHandController;

        /// <summary>
        /// Keybind to invoke <see cref="XRController.activateUsage"/>.
        /// </summary>
        [Header("Keybinds")]
        [SerializeField]
        private KeyCode m_active = KeyCode.F;

        /// <summary>
        /// Keybind to invoke <see cref="XRController.selectUsage"/>.
        /// </summary>
        [SerializeField]
        private KeyCode m_select = KeyCode.G;

        /// <summary>
        /// Keybind to invoke <see cref="XRController.uiPressUsage"/>.
        /// </summary>
        [SerializeField]
        private KeyCode m_ui = KeyCode.V;

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
            var text = $"Using {(UseLeft ? "left" : "right")} hand controller\n" +
                $"Active Usage: {m_active}\nSelect Usage: {m_select}\nUI Usage: {m_ui}";
            GUI.Label(new Rect(10, 10, 300, 300), text);
        }

        private void Update()
        {
            if(Input.GetKeyDown(m_active))
            {
                Current.Active();
            }

            if(Input.GetKeyDown(m_ui))
            {
                Current.UIPress();
            }

            if(Input.GetKeyDown(m_select))
            {
                Current.Select();
            }
        }

        private XRControllerBinding Current => UseLeft ? m_leftHand : m_rightHand;
        private bool UseLeft => Input.GetKey(KeyCode.LeftShift);
    }
}