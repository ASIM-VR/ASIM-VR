using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace AsimVr.Inputs.Examples
{
    /// <summary>
    /// Simple tape measure to measure the distance between two points.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class TapeMeasure : MonoBehaviour
    {
        private TextMeshPro m_text;
        private LineRenderer m_line;
        private Vector3 m_start;
        private Vector3 m_end;
        private bool m_measuring;

        private void Awake()
        {
            m_line = GetComponent<LineRenderer>();
            var text = new GameObject("Text");
            m_text = text.AddComponent<TextMeshPro>();
            m_text.fontSize = 12f;
            m_text.alignment = TextAlignmentOptions.Center;
            m_text.transform.SetParent(m_line.transform);
            Display(false);
        }

        private void Update()
        {
            if(m_text.gameObject.activeInHierarchy)
            {
                m_text.transform.rotation = Camera.main.transform.rotation;
            }
        }

        private void OnEnable()
        {
            //Add listener for primary trigger button to set a point.
            Input.AddListener(AsimTrigger.Primary, AsimState.Down, SetPoint);
            //Add listener for button1 to clear the current measurement.
            Input.AddListener(AsimTrigger.Button1, AsimState.Down, Clear);
        }

        private void OnDisable()
        {
            Input.RemoveListener(AsimTrigger.Primary, AsimState.Down, SetPoint);
            Input.RemoveListener(AsimTrigger.Button1, AsimState.Down, Clear);
        }

        private void Reset()
        {
            m_line = GetComponent<LineRenderer>();
            m_line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            m_line.startWidth = 0.025f;
        }

        private void OnDrawGizmos()
        {
            if(m_line != null && m_line.enabled)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(m_start, 0.05f);
                Gizmos.DrawWireSphere(m_end, 0.05f);
                Gizmos.DrawLine(m_start, m_end);
#if UNITY_EDITOR
                var text = $"{Mathf.Round(Vector3.Distance(m_start, m_end) * 100)}cm";
                UnityEditor.Handles.color = Color.blue;
                UnityEditor.Handles.Label((m_start + m_end) * 0.5f, text);
#endif
            }
        }

        private void SetPoint(XRNode node, XRRayInteractor interactor)
        {
            //Only set the current point if the triggering controller is pointing at
            //a valid surface.
            if(interactor.GetCurrentRaycastHit(out var hit))
            {
                if(!m_measuring)
                {
                    m_start = hit.point;
                }
                else
                {
                    m_end = hit.point;
                }
                Display(m_measuring);
                m_measuring = !m_measuring;
            }
        }

        private void Clear(XRNode node, XRRayInteractor interactor)
        {
            //You could limit the clear to a certain controller by
            //comparing the current node.
            Display(false);
            m_measuring = false;
        }

        private void Display(bool display)
        {
            m_line.enabled = display;
            m_text.gameObject.SetActive(display);
            if(m_line.enabled)
            {
                m_line.SetPosition(0, m_start);
                m_line.SetPosition(1, m_end);
                m_text.transform.position = (m_end + m_start) * 0.5f;
                m_text.SetText($"{Mathf.Round(Vector3.Distance(m_start, m_end) * 100)}cm");
            }
        }

        private AsimInput Input => AsimInput.Instance;
    }
}