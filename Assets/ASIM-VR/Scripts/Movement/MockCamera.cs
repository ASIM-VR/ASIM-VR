using UnityEngine;

namespace AsimVr.Movement
{
    /// <summary>
    /// Simple mouse rotation for MockHMD.
    /// </summary>
    [MockHMDOnly]
    public class MockCamera : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.position += Vector3.up * 1.5f;
        }

        private void Update()
        {
            UpdateCursor();
            if(!Cursor.visible)
            {
                transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
            }
        }

        /// <summary>
        /// Update cursor visibility.
        /// </summary>
        private void UpdateCursor()
        {
            switch(Cursor.lockState)
            {
                case CursorLockMode.None:
                    if(Input.GetMouseButton(0))
                    {
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    break;

                case CursorLockMode.Confined:
                case CursorLockMode.Locked:
                    if(Input.GetKeyDown(KeyCode.Escape))
                    {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                    break;
            }
        }
    }
}