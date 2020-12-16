using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Provide keyboard and mouse input implementation.
    /// By default right hand controller is used, hold left ctrl to use left hand controller.
    /// Triggers:
    ///     InputHelpers.Button.Trigger:            Left mouse button
    ///     InputHelpers.Button.Grip:               Right mouse button
    ///     InputHelpers.Button.PrimaryButton:      E
    ///     InputHelpers.Button.SecondaryButton:    R
    /// </summary>
    public class MockHMDInput : IAsimInput
    {
        private readonly XRRayInteractor m_rightHand;
        private readonly XRRayInteractor m_leftHand;

        private readonly XRController m_rightController;
        private readonly XRController m_leftController;

        private readonly KeyCode m_primary = KeyCode.Mouse0;
        private readonly KeyCode m_secondary = KeyCode.Mouse1;
        private readonly KeyCode m_button1 = KeyCode.E;
        private readonly KeyCode m_button2 = KeyCode.R;
        private readonly KeyCode m_axisClick = KeyCode.Q;

        public MockHMDInput(XRController right, XRController left)
        {
            m_rightController = right;
            m_leftController = left;
            m_rightHand = right.GetComponent<XRRayInteractor>();
            m_leftHand = left.GetComponent<XRRayInteractor>();
        }

        public void UpdateTrigger(InputHelpers.Button button, AsimState state, TriggerAction action)
        {
            var (controller, ray) = GetCurrent();
            switch(state)
            {
                case AsimState.Down:
                    if(GetTriggerDown(button))
                        action?.Invoke(controller, ray);
                    break;

                case AsimState.Hold:
                    if(GetTrigger(button))
                        action?.Invoke(controller, ray);
                    break;

                case AsimState.Up:
                    if(GetTriggerUp(button))
                        action?.Invoke(controller, ray);
                    break;
            }
        }

        private bool GetTriggerDown(InputHelpers.Button trigger)
        {
            return Input.GetKeyDown(TriggerToKeyCode(trigger));
        }

        private bool GetTrigger(InputHelpers.Button trigger)
        {
            return Input.GetKey(TriggerToKeyCode(trigger));
        }

        private bool GetTriggerUp(InputHelpers.Button trigger)
        {
            return Input.GetKeyUp(TriggerToKeyCode(trigger));
        }

        private KeyCode TriggerToKeyCode(InputHelpers.Button trigger)
        {
            switch(trigger)
            {
                case InputHelpers.Button.Trigger:
                    return m_primary;

                case InputHelpers.Button.Grip:
                    return m_secondary;

                case InputHelpers.Button.PrimaryButton:
                    return m_button1;

                case InputHelpers.Button.SecondaryButton:
                    return m_button2;

                case InputHelpers.Button.Primary2DAxisClick:
                    return m_axisClick;
            }
            return KeyCode.None;
        }

        private (XRController controller, XRRayInteractor ray) GetCurrent()
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                return (m_leftController, m_leftHand);
            }
            return (m_rightController, m_rightHand);
        }
    }
}