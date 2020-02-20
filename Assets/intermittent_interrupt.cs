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

public Vector3 demonPosition = new Vector3(9.5f ,3.5f, 0);
public GameObject easy;
public GameObject medium;
public GameObject hard;
public GameObject insane;
public GameObject extreme;

public GameObject current = null;
private bool invoked = false;
 void Update () {
     if(!invoked && globals.gameStarted){
        InvokeRepeating("DisableRandom", 0f, 3f);  //0s delay, repeat every 10s
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
        invoked = true;
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
                color_weights.Add(color,y_coord * y_coord);
            }
            else{
                color_weights[color] += y_coord * y_coord;
            }
        }
        
    }
    foreach(COLORS color in color_weights.Keys){
        print("color" + color + "weight" + color_weights[color]);
    }
    return color_weights;
}
    
 void DisableRandom() {
     GetColorWeights();
    int x = Random.Range (0, 4);
    if(current != null){
        Destroy(current);
        current = null;
    }

    switch (x) {
            case 0:
            current_disabled = COLORS.BLUE;
            current =  Instantiate(blue, curPosition, Quaternion.identity);
            break;
            case 1:
            current_disabled = COLORS.ORANGE;
            current =  Instantiate(orange, curPosition, Quaternion.identity);
            break;
            case 2:
            current_disabled = COLORS.YELLOW;
            current =  Instantiate(yellow, curPosition, Quaternion.identity);
            break;
            case 3:
            current_disabled = COLORS.GREEN;
            current =  Instantiate(green, curPosition, Quaternion.identity);
            break;
    }
    Debug.Log(current_disabled);

 }
}
