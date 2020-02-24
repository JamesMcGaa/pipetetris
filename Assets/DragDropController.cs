using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    private static List<GameObject> disabled = new List<GameObject>();
    private bool ready = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (globals.gameStarted && !ready) {
        disabled = new List<GameObject>();
        ready = true;
      }
      if (globals.gameRestarting && ready) {
        Restart();
      }
    }

    public static void DisablePiece(GameObject go) {
      disabled.Add(go);
      go.SetActive(false);
    }

    void Restart() {
      foreach (GameObject go in disabled) {
        go.SetActive(true);
        ready = false;
      }
    }
}
