using System.Collections;
using UnityEngine;

public class TextTimer : MonoBehaviour
{
    [SerializeField]
    private float textFadeTime = 1.5f;

    private WaitForSeconds wait;
    private Coroutine coroutine;

    private void Awake()
    {
        wait = new WaitForSeconds(textFadeTime);
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        wait = new WaitForSeconds(textFadeTime);
#endif
    }

    public void StartTimer(string text)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        InfoDisplay.Instance.SetText($"Mode: {text}");
        coroutine = StartCoroutine(WaitForClearText());
    }

    private IEnumerator WaitForClearText()
    {
        yield return wait;
        InfoDisplay.Instance.ClearText();
    }
}