using TMPro;
using UnityEngine;

public class ChangeTextColor : MonoBehaviour
{
    private TMP_Text textColor;

    private void Start()
    {
        textColor = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        float r = Random.Range(0.1f, 1.0f);
        textColor.color = new Color(r, r, r, 1.0f);
    }
}