using UnityEngine;

namespace AsimVr.Inputs
{
    /// <summary>
    /// Interface for implementing input.
    /// Mainly used by <see cref="AsimInput"/>.
    /// </summary>
    public interface IAsimInput
    {
        /// <summary>
        /// Returns true on the first frame the input is active.
        /// </summary>
        /// <param name="input">Input button.</param>
        /// <returns>Is input activated on the current frame.</returns>
        bool GetButtonDown(AsimButton input);

        /// <summary>
        /// Returns true when the input is active.
        /// </summary>
        /// <param name="input">Input button.</param>
        /// <returns>Is input active during the current frame.</returns>
        bool GetButton(AsimButton input);

        /// <summary>
        /// Returns true on the first frame the input is no longer active.
        /// </summary>
        /// <param name="input">Input button.</param>
        /// <returns>Is input deactivated on the current frame..</returns>
        bool GetButtonUp(AsimButton input);

        /// <summary>
        /// Check if any trigger is in the given state and invoke the given action.
        /// </summary>
        /// <param name="state">Trigger state.</param>
        /// <param name="action">Action to invoke.</param>
        void UpdateTrigger(AsimTrigger trigger, AsimState state, AsimInput.TriggerAction action);

        /// <summary>
        /// Current scroll delta.
        /// </summary>
        /// <returns>Current scroll delta.</returns>
        Vector2 GetScroll();
    }
}