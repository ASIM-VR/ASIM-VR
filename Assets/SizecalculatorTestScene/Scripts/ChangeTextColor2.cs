using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTextColor2 : MonoBehaviour
{
    TMP_Text textColor;

    void Start()
    {
        textColor = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float r = Random.Range(0.1f, 1.0f);
        textColor.color = new Color(r, r, r, 1.0f);
    }
}
