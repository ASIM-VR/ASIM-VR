using UnityEngine;

namespace AsimVr.Movement
{
    /// <summary>
    /// Simple mouse rotation for MockHMD.
    /// Attach to Camera Offset under XR Rig.
    /// </summary>
    [MockHMDOnly]
    public class MockCamera : MockMovementBase
    {
        /// <summary>
        /// Offset from the XR Rig root game object.
        /// </summary>
        [SerializeField]
        private float m_eyeHeight = 1.7f;

        private void Awake()
        {
            transform.position += Vector3.up * m_eyeHeight;
        }
    }
}