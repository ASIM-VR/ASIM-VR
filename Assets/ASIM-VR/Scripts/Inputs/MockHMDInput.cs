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

        private readonly KeyCode m_primary = KeyCode.Mouse0;
        private readonly KeyCode m_secondary = KeyCode.Mouse1;
        private readonly KeyCode m_button1 = KeyCode.Alpha1;
        private readonly KeyCode m_button2 = KeyCode.Alpha2;

        public MockHMDInput(XRController right, XRController left)
        {
            m_rightController = right;
            m_leftController = left;
            m_rightHand = right.GetComponent<XRRayInteractor>();
            m_leftHand = left.GetComponent<XRRayInteractor>();
        }

        public void UpdateTrigger(AsimTrigger trigger, AsimState state, TriggerAction action)
        {
            var (controller, ray) = GetCurrent();
            switch(state)
            {
                case AsimState.Down:
                    if(GetTriggerDown(trigger))
                        action?.Invoke(controller.controllerNode, ray);
                    break;

                case AsimState.Hold:
                    if(GetTrigger(trigger))
                        action?.Invoke(controller.controllerNode, ray);
                    break;

                case AsimState.Up:
                    if(GetTriggerUp(trigger))
                        action?.Invoke(controller.controllerNode, ray);
                    break;
            }
        }

        private bool GetTriggerDown(AsimTrigger trigger)
        {
            return Input.GetKeyDown(TriggerToKeyCode(trigger));
        }

        private bool GetTrigger(AsimTrigger trigger)
        {
            return Input.GetKey(TriggerToKeyCode(trigger));
        }

        private bool GetTriggerUp(AsimTrigger trigger)
        {
            return Input.GetKeyUp(TriggerToKeyCode(trigger));
        }

        private KeyCode TriggerToKeyCode(AsimTrigger trigger)
        {
            switch(trigger)
            {
                case AsimTrigger.Primary:
                    return m_primary;

                case AsimTrigger.Secondary:
                    return m_secondary;

                case AsimTrigger.Button1:
                    return m_button1;

                case AsimTrigger.Button2:
                    return m_button2;
            }
            return KeyCode.None;
        }

        private (XRController controller, XRRayInteractor ray) GetCurrent()
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                return (m_leftController, m_leftHand);
            }
            return (m_rightController, m_rightHand);
        }

        public Vector2 GetScroll()
        {
            return new Vector2(Input.mouseScrollDelta.x, Input.mouseScrollDelta.y);
        }
    }
}