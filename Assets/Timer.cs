using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    float time = 0f;
    float startingTime = 0f;
    [SerializeField] Text timerText;
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

    time += 1 * Time.deltaTime;
 
    string minutes = Mathf.Floor(time / 60).ToString("00");
    string seconds = Mathf.Floor(time % 60).ToString("00");
     
    timerText.text = "Time: " + string.Format("{0}:{1}", minutes, seconds);
    print(time);
}
}