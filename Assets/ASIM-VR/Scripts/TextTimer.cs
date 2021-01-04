using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTimer : MonoBehaviour
{
    [SerializeField]
    private InfoDisplay infoDisplay;
    private bool timerOn = false;
    [SerializeField]
    private float textFadeTime = 1.5f;
    private float currentTimer;

    private void Update() {

        if (timerOn){

            currentTimer -= Time.deltaTime;

            if (currentTimer < 0) {
                ResetTimer();
            }

        } 
    }
    
    private void ResetTimer() {
        timerOn = false;
        currentTimer = textFadeTime;
        infoDisplay.ClearText();
    }

    public void StartTimer(string timerText) {
        currentTimer = textFadeTime;
        infoDisplay.SetText("Mode: "+timerText);
        timerOn = true;
    }

}
