using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
  public DIFFICULTY difficulty;
  void OnMouseUp() {
    globals.gameStarted = true;
    globals.difficulty_level = difficulty;
  }
}
