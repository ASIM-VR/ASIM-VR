using AsimVr.Inputs.Core;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Convert <see cref="InputHelpers.IsPressed"/> into Down, Hold and Up events.
    /// </summary>
    public class XRControllerAction
    {
        /// <summary>
        /// Different trigger states.
        /// </summary>
        private readonly Dictionary<InputHelpers.Button, FakeState> m_triggerStates;

        public XRControllerAction(XRController controller)
        {
            m_triggerStates = new Dictionary<InputHelpers.Button, FakeState>();
            Controller = controller;
            Ray = controller.GetComponent<XRRayInteractor>();
        }

        /// <summary>
        /// Check if the given trigger is in a given state.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <param name="state">Target state.</param>
        /// <returns>Is the trigger in the given state.</returns>
        public bool IsActive(InputHelpers.Button button, AsimState state)
        {
            if(!m_triggerStates.ContainsKey(button))
            {
                m_triggerStates.Add(button, new FakeState());
            }
            return m_triggerStates[button].GetState(state, IsPressed(button));
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
        /// Current XR controller.
        /// </summary>
        public XRController Controller { get; }

        /// <summary>
        /// Current XR ray interactor.
        /// </summary>
        public XRRayInteractor Ray { get; }
    }
}