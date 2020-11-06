using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    public enum AsimHand
    {
        Right,
        Left
    }

    public enum AsimTrigger
    {
        Primary,
        Secondary
    }

    public enum AsimButton
    {
        Action1,
        Action2,
        Action3,
        Action4
    }

    public enum AsimState
    {
        Down,
        Hold,
        Up
    }

    /// <summary>
    /// Custom input system for ASIM-VR aiming to provide unified input system for
    /// different components requiring input.
    /// Use <see cref="Instance"/> to access the current instance.
    /// Use <see cref="AddListener(AsimButton, AsimState, Action)"/> to add listener for a given button and state.
    /// Use <see cref="RemoveListener(AsimButton, AsimState, Action)"/> to remove listener from a given button and state.
    /// Use <see cref="AddTriggerListener(AsimState, TriggerAction)"/> to add listener for controller trigger.
    /// Use <see cref="RemoveTriggerListener(AsimState, TriggerAction)"/> to remove listener from controller trigger.
    /// </summary>
    /// <example>
    /// public class InputExample : MonoBehaviour
    /// {
    ///     private void OnEnable()
    ///     {
    ///         AsimInput.Instance.AddListener(AsimButton.RightTrigger, AsimState.Down, HelloWorld);
    ///     }
    ///
    ///     private void OnDisable()
    ///     {
    ///         AsimInput.Instance.RemoveListener(AsimButton.RightTrigger, AsimState.Down, HelloWorld);
    ///     }
    ///
    ///     private void HelloWorld()
    ///     {
    ///         Debug.Log("Hello World!");
    ///     }
    /// }
    /// </example>
    [DefaultExecutionOrder(-50)]
    public class AsimInput : MonoBehaviour
    {
        public delegate void TriggerAction(XRController controller, XRRayInteractor interactor);

        [SerializeField]
        private XRRayInteractor m_rightHand;

        [SerializeField]
        private XRRayInteractor m_leftHand;

        /// <summary>
        /// Current input actions.
        /// </summary>
        private Dictionary<(AsimButton button, AsimState state), Action> m_actions;

        /// <summary>
        /// Controller trigger actions.
        /// </summary>
        private Dictionary<(AsimTrigger trigger, AsimState state), TriggerAction> m_triggerActions;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("AsimInput instance already exists!", gameObject);
                return;
            }
            m_actions = new Dictionary<(AsimButton, AsimState), Action>();
            m_triggerActions = new Dictionary<(AsimTrigger trigger, AsimState state), TriggerAction>();
            InitializeInput();
            Instance = this;
        }

        private void Update()
        {
            UpdateInput();
        }

        private void Reset()
        {
            foreach(var ray in FindObjectsOfType<XRRayInteractor>())
            {
                if(ray.gameObject.name.Contains("RightHand"))
                {
                    m_rightHand = ray;
                }

                if(ray.gameObject.name.Contains("LeftHand"))
                {
                    m_leftHand = ray;
                }
            }
        }

        /// <summary>
        /// Add input event listener for a given input and state.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <param name="state">Target state.</param>
        /// <param name="action">Action to invoke.</param>
        public void AddListener(AsimButton button, AsimState state, Action action)
        {
            var key = (button, state);
            if(!m_actions.ContainsKey(key))
            {
                m_actions.Add(key, default);
            }
            m_actions[key] += action;
        }

        /// <summary>
        /// Remove input event listener from a given input and state.
        /// </summary>
        /// <param name="button">Target button.</param>
        /// <param name="state">Target state.</param>
        /// <param name="action">Target action.</param>
        public void RemoveListener(AsimButton button, AsimState state, Action action)
        {
            var key = (button, state);
            if(m_actions.ContainsKey(key))
            {
                m_actions[key] -= action;
                if(m_actions[key] == default)
                {
                    m_actions.Remove(key);
                }
            }
        }

        public void AddTriggerListener(AsimTrigger trigger, AsimState state, TriggerAction action)
        {
            var key = (trigger, state);
            if(!m_triggerActions.ContainsKey(key))
            {
                m_triggerActions.Add(key, default);
            }
            m_triggerActions[key] += action;
        }

        public void RemoveTriggerListener(AsimTrigger trigger, AsimState state, TriggerAction action)
        {
            var key = (trigger, state);
            if(m_triggerActions.ContainsKey(key))
            {
                m_triggerActions[key] -= action;
                if(m_triggerActions[key] == default)
                {
                    m_triggerActions.Remove(key);
                }
            }
        }

        /// <summary>
        /// Get current scroll delta.
        /// </summary>
        /// <returns>Scroll delta.</returns>
        public Vector2 GetScroll()
        {
            return Input.GetScroll();
        }

        private void UpdateInput()
        {
            foreach(var action in m_triggerActions)
            {
                Input.UpdateTrigger(action.Key.trigger, action.Key.state, action.Value);
            }

            foreach(var action in m_actions)
            {
                switch(action.Key.state)
                {
                    case AsimState.Down:
                        if(Input.GetButtonDown(action.Key.button))
                        {
                            action.Value?.Invoke();
                        }
                        break;

                    case AsimState.Hold:
                        if(Input.GetButton(action.Key.button))
                        {
                            action.Value?.Invoke();
                        }
                        break;

                    case AsimState.Up:
                        if(Input.GetButtonUp(action.Key.button))
                        {
                            action.Value?.Invoke();
                        }
                        break;
                }
            }
        }

        private void InitializeInput()
        {
            if(m_rightHand == null || m_leftHand == null)
            {
                Debug.LogError("Missing XRRayInteraction references!", gameObject);
                return;
            }

            switch(XRSettings.loadedDeviceName)
            {
                //No device or mock hmd selected.
                case "":
                case "MockHMD Display":
                    //Use mouse and keyboard controls.
                    Input = new MockHMDInput(m_rightHand, m_leftHand);
                    break;

                case "OpenVR Display":
                    Input = new XRInput(m_rightHand, m_leftHand);
                    break;

                default:
                    //No input implementation.
                    Debug.LogError($"No input implementation for current device! ({XRSettings.loadedDeviceName})");
                    //Quit the application, since there is no implementation and the input manager is a singleton.
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
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