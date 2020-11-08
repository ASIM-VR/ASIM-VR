using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs
{
    public enum AsimTrigger
    {
        Primary,
        Secondary,
        Button1,
        Button2
    }

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
    public delegate void TriggerAction(XRNode node, XRRayInteractor interactor);

    /// <summary>
    /// Custom input system for ASIM-VR aiming to provide unified input system for
    /// different components requiring input.
    /// Use <see cref="Instance"/> to access the current instance.
    /// Use <see cref="AddListener(AsimTrigger, AsimState, TriggerAction)"/> to add listener for controller trigger.
    /// Use <see cref="RemoveListener(AsimTrigger, AsimState, TriggerAction)"/> to remove listener from controller trigger.
    /// </summary>
    /// <example>
    /// public class InputExample : MonoBehaviour
    /// {
    ///     private void OnEnable()
    ///     {
    ///         AsimInput.Instance.AddListener(AsimTrigger.Primary, AsimState.Down, HelloWorld);
    ///     }
    ///
    ///     private void OnDisable()
    ///     {
    ///         AsimInput.Instance.RemoveListener(AsimTrigger.Primary, AsimState.Down, HelloWorld);
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
        private Dictionary<(AsimTrigger trigger, AsimState state), TriggerAction> m_action;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("AsimInput instance already exists!", gameObject);
                return;
            }
            m_action = new Dictionary<(AsimTrigger, AsimState), TriggerAction>();
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
            foreach(var ray in FindObjectsOfType<XRController>())
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
        /// Add given action to given trigger and trigger state.
        /// </summary>
        /// <param name="trigger">Target trigger.</param>
        /// <param name="state">Target state.</param>
        /// <param name="action">Action to invoke.</param>
        public void AddListener(AsimTrigger trigger, AsimState state, TriggerAction action)
        {
            var key = (trigger, state);
            if(!m_action.ContainsKey(key))
            {
                m_action.Add(key, default);
            }
            m_action[key] += action;
        }

        /// <summary>
        /// Remove given action from given trigger and trigger state.
        /// </summary>
        /// <param name="trigger">Target trigger.</param>
        /// <param name="state">Target state.</param>
        /// <param name="action">Action to remove.</param>
        public void RemoveListener(AsimTrigger trigger, AsimState state, TriggerAction action)
        {
            var key = (trigger, state);
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
        /// Get current scroll delta.
        /// </summary>
        /// <returns>Scroll delta.</returns>
        public Vector2 GetScroll()
        {
            return Input.GetScroll();
        }

        /// <summary>
        /// Update registered actions.
        /// </summary>
        private void UpdateInput()
        {
            foreach(var action in m_action)
            {
                Input.UpdateTrigger(action.Key.trigger, action.Key.state, action.Value);
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
                    //Quit the application, since there is no valid input implementation and the input manager is a singleton.
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