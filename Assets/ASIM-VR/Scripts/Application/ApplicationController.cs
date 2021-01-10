using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    private void Update()
    {
        if(!Cursor.visible && Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    /// <summary>
    /// Quit the current application or exit play mode.
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}