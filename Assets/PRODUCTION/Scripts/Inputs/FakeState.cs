using UnityEngine;

namespace AsimVr.Inputs.Core
{
    /// <summary>
    /// Convert boolean value to up, down and hold states.
    /// This aims to mimic similar behaviour to Unitys legacy input system GetButton methods.
    /// Note: Using time to track the state may cause problems if the method is called
    ///       from multiple locations using different methods e.g. Update and LateUpdate.
    ///       However, in this case this should not mater since only the AsimInput should use these methods.
    /// </summary>
    public class FakeState
    {
        /// <summary>
        /// First frame when the button was pressed down.
        /// </summary>
        private float m_downTick;

        /// <summary>
        /// First frame after the button was released.
        /// </summary>
        private float m_upTick;

        public FakeState()
        {
            m_downTick = -1f;
            m_upTick = 0f;
        }

        public bool GetInputDown(bool down)
        {
            if(down)
            {
                if(m_upTick > m_downTick)
                {
                    m_downTick = Time.time;
                }
            }
            else if(m_upTick < m_downTick)
            {
                m_upTick = Time.time;
            }
            return m_downTick == Time.time;
        }

        public bool GetInput(bool down)
        {
            return down;
        }

        public bool GetInputUp(bool down)
        {
            if(!down)
            {
                if(m_upTick < m_downTick)
                {
                    m_upTick = Time.time;
                }
            }
            else if(m_downTick < m_upTick)
            {
                m_downTick = Time.time;
            }
            return m_upTick == Time.time;
        }

        public bool GetState(AsimState state, bool down)
        {
            switch(state)
            {
                case AsimState.Down:
                    return GetInputDown(down);

                case AsimState.Hold:
                    return GetInput(down);

                case AsimState.Up:
                    return GetInputUp(down);

                default:
                    return false;
            }
        }
    }
}