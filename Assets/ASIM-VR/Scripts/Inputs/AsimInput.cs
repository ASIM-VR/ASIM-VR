using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    public enum AsimState
    {
        Down,
        Hold,
        Up
    }

    /// <summary>
    /// Input listener action delegate.
    /// </summary>
    /// <param name="node">Node activating the trigger.</param>
    /// <param name="interactor">Trigger activator ray interactor.</param>
    public delegate void TriggerAction(XRController controller, XRRayInteractor interactor);

    /// <summary>
    /// Custom input system for ASIM-VR aiming to provide unified input system for
    /// different components requiring input.
    /// Use <see cref="Instance"/> to access the current instance.
    /// Use <see cref="AddListener(InputHelpers.Button, AsimState, TriggerAction)"/> to add listener for controller trigger.
    /// Use <see cref="RemoveListener(InputHelpers.Button, AsimState, TriggerAction)"/> to remove listener from controller trigger.
    /// </summary>
    /// <example>
    /// public class InputExample : MonoBehaviour
    /// {
    ///     private void OnEnable()
    ///     {
    ///         AsimInput.Instance.AddListener(InputHelpers.Button.Trigger, AsimState.Down, HelloWorld);
    ///     }
    ///
    ///     private void OnDisable()
    ///     {
    ///         AsimInput.Instance.RemoveListener(InputHelpers.Button.Trigger, AsimState.Down, HelloWorld);
    ///     }
    ///
    ///     private void HelloWorld(XRNode node, XRRayInteractor interactor)
    ///     {
    ///         Debug.Log("Hello World!");
    ///     }
    /// }
    /// </example>
    [DefaultExecutionOrder(-50)]
    public class AsimInput : MonoBehaviour
    {
        /// <summary>
        /// Current right hand controller.
        /// </summary>
        [SerializeField]
        private XRController m_rightHand;

        /// <summary>
        /// Current left hand controller.
        /// </summary>
        [SerializeField]
        private XRController m_leftHand;

        /// <summary>
        /// Controller trigger actions.
        /// </summary>
        private Dictionary<(InputHelpers.Button button, AsimState state), TriggerAction> m_action;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("AsimInput instance already exists!", gameObject);
                return;
            }
            m_action = new Dictionary<(InputHelpers.Button, AsimState), TriggerAction>();
            InitializeInput();
            Instance = this;
        }

        private void Update()
        {
            UpdateInput();
        }

        private void Reset()
        {
            //Try to find the current left and right hand controllers.
            foreach(var controller in FindObjectsOfType<XRController>())
            {
                if(controller.controllerNode == XRNode.RightHand)
                {
                    m_rightHand = controller;
                }

                if(controller.controllerNode == XRNode.LeftHand)
                {
                    m_leftHand = controller;
                }
            }
        }

        private void OnDestroy()
        {
            if(this == Instance)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Add given action to given trigger and trigger state.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <param name="state">Target state.</param>
        /// <param name="action">Action to invoke.</param>
        public void AddListener(InputHelpers.Button button, AsimState state, TriggerAction action)
        {
            var key = (button, state);
            if(!m_action.ContainsKey(key))
            {
                m_action.Add(key, default);
            }
            m_action[key] += action;
        }

        /// <summary>
        /// Remove given action from given trigger and trigger state.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <param name="state">Target state.</param>
        /// <param name="action">Action to remove.</param>
        public void RemoveListener(InputHelpers.Button button, AsimState state, TriggerAction action)
        {
            var key = (button, state);
            if(m_action.ContainsKey(key))
            {
                m_action[key] -= action;
                if(m_action[key] == default)
                {
                    m_action.Remove(key);
                }
            }
        }

        /// <summary>
        /// Update registered actions.
        /// </summary>
        private void UpdateInput()
        {
            foreach(var action in m_action)
            {
                Input.UpdateTrigger(action.Key.button, action.Key.state, action.Value);
            }
        }

        private void InitializeInput()
        {
            if(m_rightHand == null || m_leftHand == null)
            {
                Debug.LogError("Missing XR controller references!", gameObject);
                return;
            }

            switch(XRSettings.loadedDeviceName)
            {
                //No device or mock hmd selected.
                //Loaded device name can be empty if the selected XR plugin fails to load.
                case "":
                case "MockHMD Display":
                    //Use mouse and keyboard controls.
                    Input = new MockHMDInput(m_rightHand, m_leftHand);
                    break;

                default:
                    //Default to XR input implementation.
                    Input = new XRInput(m_rightHand, m_leftHand);
                    break;
            }
        }

        /// <summary>
        /// Current input manager instance.
        /// </summary>
        public static AsimInput Instance { get; private set; }

        /// <summary>
        /// Current input implementation.
        /// </summary>
        private IAsimInput Input { get; set; }
    }
}