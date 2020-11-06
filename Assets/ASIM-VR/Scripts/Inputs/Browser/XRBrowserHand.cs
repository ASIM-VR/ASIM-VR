using AsimVr.Inputs;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ZenFulcrum.EmbeddedBrowser;
using ZenFulcrum.EmbeddedBrowser.VR;

namespace AsimVr.Demo
{
    /// <summary>
    /// This script is extension for EmbeddedBrowser.
    /// Custom <see cref="VRBrowserHand"/> for XR-Interaction toolkit.
    /// </summary>
    public class XRBrowserHand : VRBrowserHand
    {
        /// <summary>
        /// Binding flags for finding private properties in the base class.
        /// </summary>
        private const BindingFlags Flags = BindingFlags.Instance
                                         | BindingFlags.Public
                                         | BindingFlags.NonPublic;

        /// <summary>
        /// Property info for <see cref="VRBrowserHand.DepressedButtons"/>.
        /// </summary>
        private static readonly PropertyInfo depressedButtons
            = typeof(VRBrowserHand)
                .GetProperty("DepressedButtons", Flags);

        /// <summary>
        /// Set method infor for <see cref="VRBrowserHand.Tracked"/>.
        /// </summary>
        private static readonly MethodInfo setTracked
            = typeof(VRBrowserHand)
                .GetProperty("Tracked", Flags)
                .GetSetMethod(true);

        /// <summary>
        /// Set method info for <see cref="VRBrowserHand.ScrollDelta"/>.
        /// </summary>
        private static readonly MethodInfo setScroll
            = typeof(VRBrowserHand)
                .GetProperty("ScrollDelta", Flags)
                .GetSetMethod(true);

        /// <summary>
        /// Local "DepressedButtons", since it is private in the base class.
        /// </summary>
        private MouseButton m_buttons;

        private void Awake()
        {
            if(VRInput.Impl == null)
            {
                //Inject custom input implementation to prevent error message
                //However, this implementation is overrided by this class.
                typeof(VRInput)
                    .GetProperty("Impl", BindingFlags.Static | BindingFlags.Public)
                    .SetValue(null, new XRBrowserInput());
            }
        }

        new public void OnEnable()
        {
            base.OnEnable();
            Input.AddTriggerListener(AsimTrigger.Primary, AsimState.Hold, PrimaryHold);
            Input.AddTriggerListener(AsimTrigger.Secondary, AsimState.Hold, SecondaryHold);
        }

        new public void OnDisable()
        {
            base.OnDisable();
            Input.RemoveTriggerListener(AsimTrigger.Primary, AsimState.Hold, PrimaryHold);
            Input.RemoveTriggerListener(AsimTrigger.Secondary, AsimState.Hold, SecondaryHold);
        }

        private void PrimaryHold(XRController controller, XRRayInteractor interactor)
        {
            if(controller.controllerNode == hand)
            {
                m_buttons |= MouseButton.Left;
            }
        }

        private void SecondaryHold(XRController controller, XRRayInteractor interactor)
        {
            if(controller.controllerNode == hand)
            {
                m_buttons |= MouseButton.Right;
            }
        }

        protected override void ReadInput()
        {
            //Force VRBrowserHand.Tracking on since XRNodeState.tracked is skipped.
            SetTracked(true);
            //TODO: Test in VR.
            SetDepressedButtons(m_buttons);
            SetScroll(Input.GetScroll());
            m_buttons = 0;
        }

        /// <summary>
        /// Set value for "DepressedButtons" in the base class.
        /// </summary>
        /// <param name="button">New value.</param>
        private void SetDepressedButtons(MouseButton button)
        {
            depressedButtons.SetValue(this, button);
        }

        /// <summary>
        /// Set value for "Tracked" property in the base class.
        /// </summary>
        /// <param name="value">New value.</param>
        private void SetTracked(bool value)
        {
            setTracked.Invoke(this, new object[] { value });
        }

        /// <summary>
        /// Set value for "ScrollDelta" property in the base class.
        /// </summary>
        /// <param name="vector">New value.</param>
        private void SetScroll(Vector2 vector)
        {
            setScroll.Invoke(this, new object[] { vector });
        }

        /// <summary>
        /// Current input implementation.
        /// </summary>
        private AsimInput Input => AsimInput.Instance;
    }
}