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
    if(! globals.gameStarted){
        return;
    }

    time += 1 * Time.deltaTime;
    timerText.text = "Time: " + time.ToString("0");
    print(time);
}
}