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
        private KeyCode m_rigth = KeyCode.Alpha2;

        [SerializeField]
        private KeyCode m_down = KeyCode.Alpha4;

        [SerializeField]
        private KeyCode m_left = KeyCode.Alpha4;

        [SerializeField]
        private KeyCode m_select = KeyCode.E;

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
                if(Input.GetKeyDown(m_select))
                {
                    m_menu.ActivateHighlithedSection();
                }
            }
        }

        private Vector2 Position => new Vector2
        {
            x = Input.GetKey(KeyCode.Alpha2) ? 1
              : Input.GetKey(KeyCode.Alpha4) ? -1 : 0,
            y = Input.GetKey(KeyCode.Alpha1) ? 1
              : Input.GetKey(KeyCode.Alpha3) ? -1 : 0
        };
    }
}