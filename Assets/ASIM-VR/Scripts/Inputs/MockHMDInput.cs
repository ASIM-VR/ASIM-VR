using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Provide keyboard and mouse implementation for input.
    /// Right hand trigger is left mouse button.
    /// Left hand trigger is right mouse button.
    /// </summary>
    public class MockHMDInput : IAsimInput
    {
        private readonly XRRayInteractor m_rightHand;
        private readonly XRRayInteractor m_leftHand;

        private readonly XRController m_rightController;
        private readonly XRController m_leftController;

        public MockHMDInput(XRController right, XRController left)
        {
            m_rightController = right;
            m_leftController = left;
            m_rightHand = right.GetComponent<XRRayInteractor>();
            m_leftHand = left.GetComponent<XRRayInteractor>();
        }

        public void UpdateTrigger(AsimTrigger trigger, AsimState state, TriggerAction action)
        {
            switch(state)
            {
                case AsimState.Down:
                    if(Input.GetMouseButtonDown(0))
                        action?.Invoke(m_rightController.controllerNode, m_rightHand);
                    if(Input.GetMouseButtonDown(1))
                        action?.Invoke(m_leftController.controllerNode, m_leftHand);
                    break;

                case AsimState.Hold:
                    if(Input.GetMouseButton(0))
                        action?.Invoke(m_rightController.controllerNode, m_rightHand);
                    if(Input.GetMouseButton(1))
                        action?.Invoke(m_leftController.controllerNode, m_leftHand);
                    break;

                case AsimState.Up:
                    if(Input.GetMouseButtonUp(0))
                        action?.Invoke(m_rightController.controllerNode, m_rightHand);
                    if(Input.GetMouseButtonUp(1))
                        action?.Invoke(m_leftController.controllerNode, m_leftHand);
                    break;
            }
        }

        public Vector2 GetScroll()
        {
            return new Vector2(Input.mouseScrollDelta.x, Input.mouseScrollDelta.y);
        }
    }
}