using AsimVr.Inputs.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Convert <see cref="InputHelpers.IsPressed"/> into Down, Hold and Up events.
    /// Trigger button is defined by <see cref="XRController"/>.
    /// </summary>
    public class XRControllerAction
    {
        private readonly FakeState m_primaryState;
        private readonly FakeState m_secondaryState;
        private readonly XRController m_controller;

        public XRControllerAction(XRRayInteractor ray)
        {
            m_primaryState = new FakeState();
            m_secondaryState = new FakeState();
            Ray = ray;
            if(!ray.TryGetComponent(out m_controller))
            {
                Debug.LogError("Unable to find XRController!", ray.gameObject);
            }
        }

        public bool IsActive(AsimTrigger trigger, AsimState state)
        {
            return GetFakeState(trigger).GetState(state, IsPressed(trigger));
        }

        private FakeState GetFakeState(AsimTrigger trigger)
        {
            switch(trigger)
            {
                case AsimTrigger.Primary:
                    return m_primaryState;

                case AsimTrigger.Secondary:
                    return m_secondaryState;

                default:
                    return default;
            }
        }

        private bool IsPressed(AsimTrigger trigger)
        {
            switch(trigger)
            {
                case AsimTrigger.Primary:
                    return PrimaryDown;

                case AsimTrigger.Secondary:
                    return SecondaryDown;

                default:
                    return false;
            }
        }

        public XRController Controller => m_controller;
        public XRRayInteractor Ray { get; }

        private bool PrimaryDown => Controller.inputDevice.IsPressed(Controller.selectUsage, out var down) && down;
        private bool SecondaryDown => Controller.inputDevice.IsPressed(Controller.activateUsage, out var down) && down;
    }
}