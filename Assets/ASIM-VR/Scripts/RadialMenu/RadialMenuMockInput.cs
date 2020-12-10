using UnityEngine;

namespace AsimVr.Inputs
{
    [MockHMDOnly]
    public class RadialMenuMockInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private RadialMenu m_menu;

        [Header("Keybinds")]
        [SerializeField]
        private KeyCode m_up = KeyCode.Alpha1;

        [SerializeField]
        private KeyCode m_right = KeyCode.Alpha2;

        [SerializeField]
        private KeyCode m_down = KeyCode.Alpha3;

        [SerializeField]
        private KeyCode m_left = KeyCode.Alpha4;

        private void Reset()
        {
            m_menu = FindObjectOfType<RadialMenu>();
        }

        private void Update()
        {
            m_menu.Show(Position != Vector2.zero);
            if(m_menu.gameObject.activeInHierarchy)
            {
                m_menu.SetTouchPosition(Position);
                m_menu.ActivateHighlithedSection();
            }
        }

        private Vector2 Position => new Vector2
        {
            x = Input.GetKey(m_right) ? 1
              : Input.GetKey(m_left) ? -1 : 0,
            y = Input.GetKey(m_up) ? 1
              : Input.GetKey(m_down) ? -1 : 0
        };
    }
}