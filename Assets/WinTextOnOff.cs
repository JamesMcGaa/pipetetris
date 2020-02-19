using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextOnOff : MonoBehaviour
{
    public Text WinText;

    // Update is called once per frame
    void Update()
    {
        if (globals.gameWon) {
            WinText.enabled = true;
        } else {
            WinText.enabled = false;
        }
        
        //Debug.Log(globals.gameWon);
        foreach (string key in globals.reachable_moves.Keys){
            if (int.Parse(key.Substring(key.IndexOf(',') + 1)) >= globals.BOARD_HEIGHT) {
                globals.gameWon = true;
                break;
            }
        }
    }
}