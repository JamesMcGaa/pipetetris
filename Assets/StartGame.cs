using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private float x_offset = 5.5f;
    private float y_offset = 4.0f;
    private Dictionary<DIFFICULTY, Vector3> button_positions = new Dictionary<DIFFICULTY, Vector3>();
    private Dictionary<DIFFICULTY, GameObject> start_buttons = new Dictionary<DIFFICULTY, GameObject>();
    private Dictionary<DIFFICULTY, GameObject> start_icons = new Dictionary<DIFFICULTY, GameObject>();
    public GameObject easy;
    public GameObject medium;
    public GameObject hard;
    public GameObject insane;
    public GameObject extreme;
    public GameObject button;

    void Start() {
      button_positions[DIFFICULTY.EASY] = new Vector3(-x_offset, y_offset, 0);
      button_positions[DIFFICULTY.MEDIUM] = new Vector3(x_offset, y_offset, 0);
      button_positions[DIFFICULTY.HARD] = new Vector3(0, 0, 0);
      button_positions[DIFFICULTY.INSANE] = new Vector3(-x_offset, -y_offset, 0);
      button_positions[DIFFICULTY.EXTREME] = new Vector3(x_offset, -y_offset, 0);

      foreach (DIFFICULTY diff in button_positions.Keys) {
        start_buttons[diff] = Instantiate(button, button_positions[diff], Quaternion.identity);
      }
      start_icons[DIFFICULTY.EASY] = Instantiate(easy, button_positions[DIFFICULTY.EASY], Quaternion.identity);
      start_icons[DIFFICULTY.MEDIUM] = Instantiate(medium, button_positions[DIFFICULTY.MEDIUM], Quaternion.identity);
      start_icons[DIFFICULTY.HARD] = Instantiate(hard, button_positions[DIFFICULTY.HARD], Quaternion.identity);
      start_icons[DIFFICULTY.INSANE] = Instantiate(insane, button_positions[DIFFICULTY.INSANE], Quaternion.identity);
      start_icons[DIFFICULTY.EXTREME] = Instantiate(extreme, button_positions[DIFFICULTY.EXTREME], Quaternion.identity);
    }

    void Update() {
      if (globals.gameStarted) {
        foreach (DIFFICULTY diff in start_buttons.Keys) {
          Destroy(start_buttons[diff]);
        }
        foreach (DIFFICULTY diff in start_icons.Keys) {
          Destroy(start_icons[diff]);
        }
        enabled = false;
      }
    }

}
