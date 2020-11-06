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
        /// Check if any trigger is in the given state and invoke the given action.
        /// </summary>
        /// <param name="state">Trigger state.</param>
        /// <param name="action">Action to invoke.</param>
        void UpdateTrigger(AsimTrigger trigger, AsimState state, TriggerAction action);

        /// <summary>
        /// Current scroll delta.
        /// </summary>
        /// <returns>Current scroll delta.</returns>
        Vector2 GetScroll();
    }
}