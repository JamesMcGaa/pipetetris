using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void OnMouseUp() {
      globals.gameStarted = true;
      Destroy(gameObject);
    }
}
