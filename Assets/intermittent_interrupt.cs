using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intermittent_interrupt : MonoBehaviour
{

private GameObject demonIcon;
private GameObject disableIcon;


public static COLORS current_disabled;

public Vector3 curPosition = new Vector3(9.5f ,3.5f, 0);
public GameObject orange;
public GameObject yellow;
public GameObject green;
public GameObject blue;
public GameObject disable;
private Dictionary<COLORS, GameObject> squares = new Dictionary<COLORS, GameObject>();

public Vector3 demonPosition = new Vector3(9.5f ,3.5f, 0);
public GameObject easy;
public GameObject medium;
public GameObject hard;
public GameObject insane;
public GameObject extreme;

public GameObject current = null;
private bool displayed = false;

void Start() {
  squares[COLORS.ORANGE] = orange;
  squares[COLORS.YELLOW] = yellow;
  squares[COLORS.GREEN] = green;
  squares[COLORS.BLUE] = blue;
}

void Update () {
   if(!displayed && globals.gameStarted){
      switch(globals.difficulty_level)
      {
        case DIFFICULTY.EASY:
          demonIcon = Instantiate(easy, demonPosition, Quaternion.identity);
          break;
        case DIFFICULTY.MEDIUM:
          demonIcon = Instantiate(medium, demonPosition, Quaternion.identity);
          break;
        case DIFFICULTY.HARD:
          demonIcon = Instantiate(hard, demonPosition, Quaternion.identity);
          break;
        case DIFFICULTY.INSANE:
          demonIcon = Instantiate(insane, demonPosition, Quaternion.identity);
          break;
        case DIFFICULTY.EXTREME:
          demonIcon = Instantiate(extreme, demonPosition, Quaternion.identity);
          break;
        default:
          break;
      }
      disableIcon = Instantiate(disable, curPosition, Quaternion.identity);
      displayed = true;
   }
   if(globals.gameLoaded && !globals.gameWon && !globals.gameLost) {
     if (globals.turnsTaken >= globals.timesDisabled) {
       globals.timesDisabled++;
       Disable();
     }
   }
}
Dictionary<COLORS, int> GetColorWeights() {
    Dictionary<COLORS, int> color_weights = new Dictionary<COLORS, int>();

    foreach(string coord in globals.reachable_moves.Keys){
        // reachable and on screen but not occupied
        int comma_index = coord.IndexOf(',');
        int x_coord;
        int y_coord;
        int.TryParse(coord.Substring(comma_index+1), out y_coord);
        int.TryParse(coord.Substring(0,comma_index), out x_coord);


        bool inbounds = x_coord >= 0 && x_coord < globals.BOARD_WIDTH && y_coord >= 0 && y_coord < globals.BOARD_HEIGHT;
        if(! globals.occupied_squares.ContainsKey(coord) && inbounds){
            //print("game loaded" + globals.gameLoaded);
            //print("coord" + coord);
            print(globals.board_colors[coord]);
            COLORS color = globals.board_colors[coord];

            if(! color_weights.ContainsKey(color)){
                color_weights.Add(color,(x_coord+1) * (x_coord+1));
            }
            else{
                color_weights[color] += (x_coord+1) * (x_coord+1);
            }
        }

    }
    foreach(COLORS color in color_weights.Keys){
        print("color" + color + "weight" + color_weights[color]);
    }
    return color_weights;
}

 void Disable() {
    Dictionary<COLORS, int> color_weights = GetColorWeights();
    int total = 0;
    foreach (COLORS color in color_weights.Keys) {
      total += color_weights[color];
    }
    int x = Random.Range(0, total);
    if(current != null){
        Destroy(current);
        current = null;
    }
    int bottom = 0;
    foreach (COLORS color in color_weights.Keys) {
      if (x >= bottom && x < bottom + color_weights[color]) {
        current_disabled = color;
        current =  Instantiate(squares[color], curPosition, Quaternion.identity);
      }
      bottom += color_weights[color];
    }
    Debug.Log(current_disabled);

 }
}
