using AsimVr.Inputs.Core;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Convert boolean into Down, Hold and Up events.
    /// </summary>
    public class XRInputAction
    {
        public delegate bool UpdateAction();

        private readonly FakeState m_state;
        private readonly UpdateAction m_action;

        public XRInputAction(UpdateAction action)
        {
            m_state = new FakeState();
            m_action = action;
        }

        public bool GetInputDown()
        {
            return m_state.GetInputDown(m_action.Invoke());
        }

        public bool GetInput()
        {
            return m_state.GetInput(m_action.Invoke());
        }

        public bool GetInputUp()
        {
            return m_state.GetInput(m_action.Invoke());
        }
    }
}