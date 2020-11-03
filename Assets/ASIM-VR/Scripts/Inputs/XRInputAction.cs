using UnityEngine;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Convert boolean into Down, Hold and Up events.
    /// Note: Using time to track the state may cause problems if the method is called
    ///       from multiple locations using different methods e.g. Update and LateUpdate.
    ///       However, in this case this should not mater since only the AsimInput should use these methods.
    /// </summary>
    public class XRInputAction
    {
        public delegate bool UpdateAction();

        private readonly UpdateAction m_action;
        private float m_downTick;
        private float m_upTick;

        public XRInputAction(UpdateAction action)
        {
            m_action = action;
            m_upTick = 1f;
        }

        public bool GetInputDown()
        {
            if(m_action.Invoke())
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

        public bool GetInput()
        {
            return m_action.Invoke();
        }

        public bool GetInputUp()
        {
            if(!m_action.Invoke())
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
    }
}