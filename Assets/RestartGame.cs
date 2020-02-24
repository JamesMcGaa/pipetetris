using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    void OnMouseUp() {
      if (!globals.gameStarted) {
        return;
      }
      globals.gameRestarting = true;
    }
}
