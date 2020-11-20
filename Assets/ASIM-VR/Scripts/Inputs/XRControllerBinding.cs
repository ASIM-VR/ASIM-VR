using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Core
{
    /// <summary>
    /// Use XR Controller test methods to simulate XR input with public methods.
    /// </summary>
    public class XRControllerBinding
    {
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance;
        private static readonly MethodInfo UpdateType = typeof(XRController).GetMethod("UpdateInteractionType", Flags);

        private readonly XRController m_controller;

        public XRControllerBinding(XRController controller)
        {
            m_controller = controller;
        }

        public void Select()
        {
            Invoke("select");
        }

        public void Active()
        {
            Invoke("activate");
        }

        public void UIPress()
        {
            Invoke("uiPress");
        }

        private void Invoke(string name)
        {
            UpdateType.Invoke(m_controller, new object[] { InteractionTypes[name], true });
        }

        /// <summary>
        /// Get dictionary containing <see cref="XRController.InteractionTypes"/>.
        /// </summary>
        /// <returns>Dictionary containing enum names and values.</returns>
        private static Dictionary<string, object> GetInteractionTypes()
        {
            var result = new Dictionary<string, object>();
            var type = typeof(XRController)
                .GetNestedTypes(Flags)
                .ToList()
                .First(x => x.Name == "InteractionTypes");
            foreach(object value in Enum.GetValues(type))
            {
                result.Add(Enum.GetName(type, value), value);
            }
            return result;
        }

        private static Dictionary<string, object> InteractionTypes { get; } = GetInteractionTypes();
    }
}