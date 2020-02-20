using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour {
    float time = 0f;
    float startingTime = 0f;
    private bool timerDisplayed = false;
    private TextMeshPro timer;
    public Vector3 timerPosition = new Vector3(-11.5f, -6.5f, 0);
    public TextMeshPro timerPrefab;
    void Start() {
        time = startingTime;
    }

void Update()
{
    // if(timerText == null){
    //     print("null");

    // }
    if(! globals.gameStarted || globals.gameLost || globals.gameWon){
        return;
    }

    if (globals.gameStarted && !timerDisplayed) {
      timer = Instantiate(timerPrefab, timerPosition, Quaternion.identity);
      timerDisplayed = true;
    }
    time += 1 * Time.deltaTime;

    string minutes = Mathf.Floor(time / 60).ToString("00");
    string seconds = Mathf.Floor(time % 60).ToString("00");

    timer.text = "Time: " + string.Format("{0}:{1}", minutes, seconds);
}
}
