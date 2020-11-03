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
        /// Get raycast hit from the given hand.
        /// </summary>
        /// <param name="hand">Target hand.</param>
        /// <param name="hit">Current raycast hit.</param>
        /// <returns>Is the raycast hit valid.</returns>
        bool GetRay(AsimHand hand, out RaycastHit hit);

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
        /// Current scroll delta.
        /// </summary>
        /// <returns>Current scroll delta.</returns>
        Vector2 GetScroll();
    }
}