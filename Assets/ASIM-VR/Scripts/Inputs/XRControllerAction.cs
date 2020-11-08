using AsimVr.Inputs.Core;
using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Convert <see cref="InputHelpers.IsPressed"/> into Down, Hold and Up events.
    /// Triggers:
    ///     Primary:    <see cref="XRController.activateUsage"/>
    ///     Secondary:  <see cref="XRController.selectUsage"/>
    ///     Button1:    <see cref="InputHelpers.Button.PrimaryButton"/>
    ///     Button2:    <see cref="InputHelpers.Button.SecondaryButton"/>
    /// </summary>
    public class XRControllerAction
    {
        /// <summary>
        /// Different trigger states.
        /// </summary>
        private readonly Dictionary<AsimTrigger, FakeState> m_triggerStates;

        public XRControllerAction(XRController controller)
        {
            m_triggerStates = new Dictionary<AsimTrigger, FakeState>();
            foreach(AsimTrigger trigger in Enum.GetValues(typeof(AsimTrigger)))
            {
                m_triggerStates.Add(trigger, new FakeState());
            }
            Controller = controller;
            Ray = controller.GetComponent<XRRayInteractor>();
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
                    return IsPressed(InputHelpers.Button.PrimaryButton);

                case AsimTrigger.Button2:
                    return IsPressed(InputHelpers.Button.SecondaryButton);

                default:
                    return false;
            }
        }

        /// <summary>
        /// Is the current button currently active.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <returns>Is button active.</returns>
        public bool IsPressed(InputHelpers.Button button)
        {
            return Controller.inputDevice.IsPressed(button, out var down) && down;
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