using AsimVr.Inputs.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Convert <see cref="InputHelpers.IsPressed"/> into Down, Hold and Up events.
    /// Triggers:
    ///     Primary:    <see cref="XRController.activateUsage"/>
    ///     Secondary:  <see cref="XRController.selectUsage"/>
    ///     Button1:    <see cref="m_button1"/>
    ///     Button2:    <see cref="m_button2"/>
    /// </summary>
    public class XRControllerAction
    {
        /// <summary>
        /// Different trigger states.
        /// </summary>
        private readonly Dictionary<AsimTrigger, FakeState> m_triggerStates;

        private readonly InputHelpers.Button m_button1 = InputHelpers.Button.PrimaryButton;
        private readonly InputHelpers.Button m_button2 = InputHelpers.Button.SecondaryButton;

        public XRControllerAction(XRController controller)
        {
            m_triggerStates = new Dictionary<AsimTrigger, FakeState>();
            foreach(AsimTrigger trigger in Enum.GetValues(typeof(AsimTrigger)))
            {
                m_triggerStates.Add(trigger, new FakeState());
            }
            Controller = controller;
            Ray = controller.GetComponent<XRRayInteractor>();
            ValidateController();
        }

        /// <summary>
        /// Check if the given trigger is in a given state.
        /// </summary>
        /// <param name="trigger">Target trigger.</param>
        /// <param name="state">Target state.</param>
        /// <returns>Is the trigger in the given state.</returns>
        public bool IsActive(AsimTrigger trigger, AsimState state)
        {
            return m_triggerStates[trigger].GetState(state, IsPressed(trigger));
        }

        /// <summary>
        /// Get current press state for given trigger.
        /// </summary>
        /// <param name="trigger">Target trigger.</param>
        /// <returns>Is trigger active.</returns>
        private bool IsPressed(AsimTrigger trigger)
        {
            //Note: Changing input mapping for ASIM-VR should happen here.
            switch(trigger)
            {
                case AsimTrigger.Primary:
                    return IsPressed(Controller.selectUsage);

                case AsimTrigger.Secondary:
                    return IsPressed(Controller.activateUsage);

                case AsimTrigger.Button1:
                    return IsPressed(m_button1);

                case AsimTrigger.Button2:
                    return IsPressed(m_button2);

                default:
                    return false;
            }
        }

        /// <summary>
        /// Is the current button currently active.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <returns>Is button active.</returns>
        private bool IsPressed(InputHelpers.Button button)
        {
            return Controller.inputDevice.IsPressed(button, out var down) && down;
        }

        /// <summary>
        /// Check if the current controller supports the current buttons.
        /// </summary>
        private void ValidateController()
        {
            //IsPressed is badly documented but expecting it to work like TryGetFeatureValue
            //i.e. returning true if the feature information is retrieved. It is then assumed
            //that the feature is not supported if it can not be retrieved.
            if(!Controller.inputDevice.IsPressed(Controller.selectUsage, out _))
            {
                Debug.LogError($"XRController does not support button '{Controller.selectUsage}'");
            }
            if(!Controller.inputDevice.IsPressed(Controller.activateUsage, out _))
            {
                Debug.LogError($"XRController does not support button '{Controller.activateUsage}'");
            }
            if(!Controller.inputDevice.IsPressed(m_button1, out _))
            {
                Debug.LogError($"XRController does not support button '{m_button1}'");
            }
            if(!Controller.inputDevice.IsPressed(m_button2, out _))
            {
                Debug.LogError($"XRController does not support button '{m_button2}'");
            }
        }

        /// <summary>
        /// Current XR controller.
        /// </summary>
        public XRController Controller { get; }

        /// <summary>
        /// Current XR ray interactor.
        /// </summary>
        public XRRayInteractor Ray { get; }
    }
}