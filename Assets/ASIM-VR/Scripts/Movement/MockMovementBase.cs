using UnityEngine;

namespace AsimVr.Movement
{
    /// <summary>
    /// Base class for MockMHD movement.
    /// Rotates camera and hides/shows cursor.
    /// </summary>
    public abstract class MockMovementBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void Update()
        {
            UpdateCursorLockMode();
            if(!Cursor.visible)
            {
                //Rotate camera around the current transform.
                var x = transform.eulerAngles.x - Input.GetAxis("Mouse Y");
                //Clamp euler angle between -90-90.
                x = x < 180 ? Mathf.Clamp(x, -1, 90) : Mathf.Clamp(x, 270, 361);
                transform.eulerAngles = new Vector3(x, transform.eulerAngles.y + Input.GetAxis("Mouse X"), 0f);
            }
        }

        /// <summary>
        /// Update cursor visibility.
        /// </summary>
        protected void UpdateCursorLockMode()
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