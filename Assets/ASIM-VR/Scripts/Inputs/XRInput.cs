using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    public class XRInput : IAsimInput
    {
        private readonly XRRayInteractor m_rightRay;
        private readonly XRRayInteractor m_leftRay;

        private readonly InputDevice m_rightController;
        private readonly InputDevice m_leftController;

        private Dictionary<AsimButton, XRInputAction> m_inputActions;

        public XRInput(XRRayInteractor right, XRRayInteractor left)
        {
            m_rightRay = right;
            m_leftRay = left;
            m_rightController = right.GetComponent<XRController>().inputDevice;
            m_leftController = left.GetComponent<XRController>().inputDevice;

            m_inputActions = new Dictionary<AsimButton, XRInputAction>
            {
                {
                    AsimButton.RightTrigger,
                    new XRInputAction(() => GetDeviceButton(m_rightController, CommonUsages.triggerButton))
                },
                {
                    AsimButton.LeftTrigger,
                    new XRInputAction(() => GetDeviceButton(m_leftController, CommonUsages.triggerButton))
                }
            };
        }

        public bool GetButton(AsimButton input)
        {
            if(m_inputActions.TryGetValue(input, out var current))
            {
                return current.GetInput();
            }
            return false;
        }

        public bool GetButtonDown(AsimButton input)
        {
            if(m_inputActions.TryGetValue(input, out var current))
            {
                return current.GetInputDown();
            }
            return false;
        }

        public bool GetButtonUp(AsimButton input)
        {
            if(m_inputActions.TryGetValue(input, out var current))
            {
                return current.GetInputUp();
            }
            return false;
        }

        public bool GetRay(AsimHand hand, out RaycastHit hit)
        {
            switch(hand)
            {
                case AsimHand.Right:
                    return m_rightRay.GetCurrentRaycastHit(out hit);

                case AsimHand.Left:
                    return m_leftRay.GetCurrentRaycastHit(out hit);
            }

            hit = default;
            return false;
        }

        public Vector2 GetScroll()
        {
            throw new NotImplementedException();
        }

        private bool GetDeviceButton(InputDevice device, InputFeatureUsage<bool> feature)
        {
            return device.TryGetFeatureValue(feature, out var value) && value;
        }
    }
}