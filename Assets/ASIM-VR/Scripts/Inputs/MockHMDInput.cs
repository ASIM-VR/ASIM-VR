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

        public MockHMDInput(XRRayInteractor right, XRRayInteractor left)
        {
            m_rightHand = right;
            m_leftHand = left;
        }

        public bool GetButton(AsimButton input)
        {
            switch(input)
            {
                case AsimButton.RightTrigger:
                    return Input.GetMouseButton(0);

                case AsimButton.LeftTrigger:
                    return Input.GetMouseButton(1);

                default:
                    return false;
            }
        }

        public bool GetButtonDown(AsimButton input)
        {
            switch(input)
            {
                case AsimButton.RightTrigger:
                    return Input.GetMouseButtonDown(0);

                case AsimButton.LeftTrigger:
                    return Input.GetMouseButtonDown(1);

                default:
                    return false;
            }
        }

        public bool GetButtonUp(AsimButton input)
        {
            switch(input)
            {
                case AsimButton.RightTrigger:
                    return Input.GetMouseButtonUp(0);

                case AsimButton.LeftTrigger:
                    return Input.GetMouseButtonUp(1);

                default:
                    return false;
            }
        }

        public bool GetRay(AsimHand hand, out RaycastHit hit)
        {
            switch(hand)
            {
                case AsimHand.Right:
                    return m_rightHand.GetCurrentRaycastHit(out hit);

                case AsimHand.Left:
                    return m_leftHand.GetCurrentRaycastHit(out hit);
            }

            hit = default;
            return false;
        }

        public Vector2 GetScroll()
        {
            return new Vector2(Input.mouseScrollDelta.x, Input.mouseScrollDelta.y);
        }
    }
}