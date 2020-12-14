using AsimVr.Inputs;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

#if EmbeddedBrowser

using ZenFulcrum.EmbeddedBrowser;
using ZenFulcrum.EmbeddedBrowser.VR;

#endif

namespace AsimVr.Demo
{
#if EmbeddedBrowser

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

        /// <summary>
        /// Current scroll delta.
        /// </summary>
        private Vector2 m_scroll;

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
            Input.AddListener(InputHelpers.Button.Trigger, AsimState.Hold, PrimaryHold);
            Input.AddListener(InputHelpers.Button.Grip, AsimState.Hold, SecondaryHold);
            Input.AddListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Hold, UpdateScroll);
        }

        new public void OnDisable()
        {
            base.OnDisable();
            Input.RemoveListener(InputHelpers.Button.Trigger, AsimState.Hold, PrimaryHold);
            Input.RemoveListener(InputHelpers.Button.Grip, AsimState.Hold, SecondaryHold);
            Input.RemoveListener(InputHelpers.Button.Primary2DAxisTouch, AsimState.Hold, UpdateScroll);
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

        private void UpdateScroll(XRController controller, XRRayInteractor interactor)
        {
            if(controller.controllerNode == hand &&
               controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out var value))
            {
                m_scroll = value;
            }
        }

        protected override void ReadInput()
        {
            //Force VRBrowserHand.Tracking on since XRNodeState.tracked is skipped.
            //NOTE: If using real VR device does not change the tracked state it could
            //      be used to enable/disable interaction with the browsers.
            //TODO: Test in VR.
            SetTracked(true);
            SetDepressedButtons(m_buttons);
            SetScroll(m_scroll);
            m_buttons = 0;
            m_scroll = Vector2.zero;
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

#else
    public class XRBrowserHand : MonoBehaviour
    {
    }
#endif
}