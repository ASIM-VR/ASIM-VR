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

        private readonly XRController m_rightController;
        private readonly XRController m_leftController;

        private readonly List<XRControllerAction> m_controllers;

        private readonly Dictionary<AsimButton, XRInputAction> m_inputActions;

        public XRInput(XRRayInteractor right, XRRayInteractor left)
        {
            m_rightRay = right;
            m_leftRay = left;
            m_inputActions = new Dictionary<AsimButton, XRInputAction>();
            m_controllers = new List<XRControllerAction>
            {
                new XRControllerAction(right),
                new XRControllerAction(left)
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

        public Vector2 GetScroll()
        {
            throw new NotImplementedException();
        }

        public void UpdateTrigger(AsimTrigger trigger, AsimState state, AsimInput.TriggerAction action)
        {
            foreach(var controller in m_controllers)
            {
                if(controller.IsActive(trigger, state))
                {
                    action.Invoke(controller.Controller, controller.Ray);
                }
            }
        }
    }
}