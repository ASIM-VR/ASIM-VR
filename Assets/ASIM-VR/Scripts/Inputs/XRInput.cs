using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    public class XRInput : IAsimInput
    {
        private readonly List<XRControllerAction> m_controllers;

        public XRInput(params XRController[] controllers)
        {
            m_controllers = new List<XRControllerAction>();
            foreach(var controller in controllers)
            {
                m_controllers.Add(new XRControllerAction(controller));
            }
        }

        public void UpdateTrigger(InputHelpers.Button button, AsimState state, TriggerAction action)
        {
            foreach(var controller in m_controllers)
            {
                if(controller.IsActive(button, state))
                {
                    action.Invoke(controller.Controller, controller.Ray);
                }
            }
        }

        public Vector2 GetScroll()
        {
            return Vector2.zero;
        }
    }
}