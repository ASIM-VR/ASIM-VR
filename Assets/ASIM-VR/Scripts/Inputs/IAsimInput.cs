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
        void UpdateTrigger(InputHelpers.Button button, AsimState state, TriggerAction action);
    }
}