using UnityEngine;

namespace AsimVr.Movement
{
    /// <summary>
    /// Simple free camera movement for MockHMD.
    /// </summary>
    [MockHMDOnly]
    public class MockMovement : MockMovementBase
    {
        [SerializeField]
        private float m_speed = 4f;

        protected override void Update()
        {
            base.Update();
            if(!Cursor.visible)
            {
                transform.position += transform.forward * Vertical * Time.deltaTime;
                transform.position += transform.right * Horizontal * Time.deltaTime;
            }
        }

        private float Vertical => Input.GetAxis("Vertical") * m_speed;
        private float Horizontal => Input.GetAxis("Horizontal") * m_speed;
    }
}