using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class drag_drop : MonoBehaviour
 {
     private Vector3 screenPoint;
     private Vector3 offset;
     private Vector3 original_loc;
     private TextMeshPro text;
     private bool pressed = false;
     private int times_rotated_cw = 0;
     private int stock;
     private COLORS color;
     private bool textDisplayed = false;
     private Vector3 textPosition;
     private Vector3 textOffset = new Vector3(-1.2f, 0, 0);
     private Vector3 straightTextOffset = new Vector3(-0.75f, 0, 0);
     private int startingStock = 0;

     public static float X_BOTTOM_LEFT_CORNER = -7.5f;
     public static float Y_BOTTOM_LEFT_CORNER = -5.5f;

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public TextMeshPro myText;
    public GameObject myPrefab;
    // This script will simply instantiate the Prefab when the game starts.

    //https://www.colorcombos.com/color-schemes/2/ColorCombo2.html

    void Start(){
        original_loc = gameObject.transform.position;
        if (gameObject.name.Contains("Straight")){
          startingStock = 5;
          textOffset = straightTextOffset;
        } else if (gameObject.name.Contains("Split")){
          startingStock = 3;
        } else if (gameObject.name.Contains("Turn")){
          startingStock = 10;
        }

        if (gameObject.name.Contains("Blue")) {
          color = COLORS.BLUE;
        } else if (gameObject.name.Contains("Orange")) {
          color = COLORS.ORANGE;
        } else if (gameObject.name.Contains("Yellow")) {
          color = COLORS.YELLOW;
        } else if (gameObject.name.Contains("Green")) {
          color = COLORS.GREEN;
        }

        textPosition = gameObject.transform.position + textOffset;
        stock = startingStock;
        globals.piece_color_counts[color] += startingStock;
        globals.numPieces += startingStock;
    }

     void Update()
     {
       if (!textDisplayed && globals.gameStarted) {
          text = Instantiate(myText, textPosition, Quaternion.identity);
          text.text = stock.ToString();
          textDisplayed = true;
       }
       if((Input.GetMouseButtonDown(1) || Input.GetKeyUp("space")) && pressed){ //right click
          transform.Rotate(0,0,-90); //Clockwise
          times_rotated_cw = (times_rotated_cw + 1) % 4;
       }
     }

     void OnSpaceDown() {

     }

     void OnMouseDown() {
         //start dragging the object, saving its offset
        if (!globals.gameStarted)
          return;
        pressed = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
     }

     void OnMouseUp() {
         //return the dragged generator to its original state
         Debug.Log(gameObject.name);
         pressed = false;
         transform.position = original_loc;
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;


        //check if this is a legal move
         int x_index = (int)Mathf.Round(curPosition.x - X_BOTTOM_LEFT_CORNER);
         int y_index = (int)Mathf.Round(curPosition.y - Y_BOTTOM_LEFT_CORNER);
        //  Debug.Log(x_index.ToString() + "," + y_index.ToString());
         string coord = x_index.ToString() + "," + y_index.ToString();
         if(globals.reachable_moves.ContainsKey(coord)
         && stock > 0
         && x_index >= 0 && y_index >= 0 && x_index < globals.BOARD_WIDTH && y_index < globals.BOARD_HEIGHT
         && check_orientation(x_index, y_index, globals.reachable_moves[coord])
         && !globals.occupied_squares.ContainsKey(coord)
         && check_color(x_index, y_index)
         ){
            //instantiate a new object
            curPosition.x = Mathf.Round(curPosition.x - .5f) + .5f;
            curPosition.y = Mathf.Round(curPosition.y - .5f) + .5f;
            Instantiate(myPrefab, curPosition, gameObject.transform.rotation);

            //TODO: update reachable and occupied_squares
            update_reachable(x_index, y_index, coord);
            globals.occupied_squares.Add(coord, 1);
            Destroy(globals.color_squares[coord]);
            update_victory();
            update_stock();
            globals.turnsTaken++;
         }

        //reset state
        gameObject.transform.rotation = Quaternion.identity;
        times_rotated_cw = 0;
        return;

     }

     void OnMouseDrag()
     {
         //creates the visual dragging effect
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
         curPosition.x = Mathf.Round(curPosition.x - .5f) + .5f;
         curPosition.y = Mathf.Round(curPosition.y - .5f) + .5f;
         transform.position = curPosition;
     }

     void update_reachable(int x_index, int y_index, string coord) {
       if (gameObject.name.Contains("Straight")){
         if (times_rotated_cw % 2 == 0) {
           string newcoord1 = x_index.ToString() + "," + (y_index+1).ToString();
           string newcoord2 = x_index.ToString() + "," + (y_index-1).ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.BOTTOM);
           add_reachable(newcoord2, FROM_DIRECTIONS.TOP);
         }
         else if (times_rotated_cw % 2 == 1) {
           string newcoord1 = (x_index+1).ToString() + "," + y_index.ToString();
           string newcoord2 = (x_index-1).ToString() + "," + y_index.ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.LEFT);
           add_reachable(newcoord2, FROM_DIRECTIONS.RIGHT);
         }
       }
       else if (gameObject.name.Contains("Split")){
         if (times_rotated_cw == 0) { // to bottom, left, top
           string newcoord1 = x_index.ToString() + "," + (y_index-1).ToString();
           string newcoord2 = (x_index-1).ToString() + "," + y_index.ToString();
           string newcoord3 = x_index.ToString() + "," + (y_index+1).ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.TOP);
           add_reachable(newcoord2, FROM_DIRECTIONS.RIGHT);
           add_reachable(newcoord3, FROM_DIRECTIONS.BOTTOM);
         }
         else if (times_rotated_cw == 1) { // to left, top, right
           string newcoord1 = (x_index-1).ToString() + "," + y_index.ToString();
           string newcoord2 = x_index.ToString() + "," + (y_index+1).ToString();
           string newcoord3 = (x_index+1).ToString() + "," + y_index.ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.RIGHT);
           add_reachable(newcoord2, FROM_DIRECTIONS.BOTTOM);
           add_reachable(newcoord3, FROM_DIRECTIONS.LEFT);
         }
         else if (times_rotated_cw == 2) { // to top, right, bottom
           string newcoord1 = x_index.ToString() + "," + (y_index+1).ToString();
           string newcoord2 = (x_index+1).ToString() + "," + y_index.ToString();
           string newcoord3 = x_index.ToString() + "," + (y_index-1).ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.BOTTOM);
           add_reachable(newcoord2, FROM_DIRECTIONS.LEFT);
           add_reachable(newcoord3, FROM_DIRECTIONS.TOP);
         }
         else if (times_rotated_cw == 3) { // to right, bottom, left
           string newcoord1 = (x_index+1).ToString() + "," + y_index.ToString();
           string newcoord2 = x_index.ToString() + "," + (y_index-1).ToString();
           string newcoord3 = (x_index-1).ToString() + "," + y_index.ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.LEFT);
           add_reachable(newcoord2, FROM_DIRECTIONS.TOP);
           add_reachable(newcoord3, FROM_DIRECTIONS.RIGHT);
         }
       }
       else if (gameObject.name.Contains("Turn")){
         if (times_rotated_cw == 0) { // to bottom, left
           string newcoord1 = x_index.ToString() + "," + (y_index-1).ToString();
           string newcoord2 = (x_index-1).ToString() + "," + y_index.ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.TOP);
           add_reachable(newcoord2, FROM_DIRECTIONS.RIGHT);
         }
         else if (times_rotated_cw == 1) { // to left, top
           string newcoord1 = (x_index-1).ToString() + "," + y_index.ToString();
           string newcoord2 = x_index.ToString() + "," + (y_index+1).ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.RIGHT);
           add_reachable(newcoord2, FROM_DIRECTIONS.BOTTOM);
         }
         else if (times_rotated_cw == 2) { // to top, right
           string newcoord1 = x_index.ToString() + "," + (y_index+1).ToString();
           string newcoord2 = (x_index+1).ToString() + "," + y_index.ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.BOTTOM);
           add_reachable(newcoord2, FROM_DIRECTIONS.LEFT);
         }
         else if (times_rotated_cw == 3) { // to right, bottom
           string newcoord1 = (x_index+1).ToString() + "," + y_index.ToString();
           string newcoord2 = x_index.ToString() + "," + (y_index-1).ToString();
           add_reachable(newcoord1, FROM_DIRECTIONS.LEFT);
           add_reachable(newcoord2, FROM_DIRECTIONS.TOP);
         }
       }
     }

     void add_reachable(string coord, FROM_DIRECTIONS dir) {
       if (globals.reachable_moves.ContainsKey(coord)) {
         globals.reachable_moves[coord].Add(dir);
       } else {
         List<FROM_DIRECTIONS> list = new List<FROM_DIRECTIONS>();
         list.Add(dir);
         globals.reachable_moves.Add(coord, list);
       }
     }

     bool check_orientation(int x_index, int y_index, List<FROM_DIRECTIONS> dirs){
        if (gameObject.name.Contains("Straight")){
          if (dirs.Contains(FROM_DIRECTIONS.BOTTOM) || dirs.Contains(FROM_DIRECTIONS.TOP)) {
            if (times_rotated_cw % 2 == 0) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.LEFT) || dirs.Contains(FROM_DIRECTIONS.RIGHT)) {
            if (times_rotated_cw % 2 == 1) {
              return true;
            }
          }
        }
        else if (gameObject.name.Contains("Split")){
          if (dirs.Contains(FROM_DIRECTIONS.BOTTOM)) {
            if (times_rotated_cw != 1) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.LEFT)) {
            if (times_rotated_cw != 2) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.TOP)) {
            if (times_rotated_cw != 3) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.RIGHT)) {
            if (times_rotated_cw != 0) {
              return true;
            }
          }
        }

        else if (gameObject.name.Contains("Turn")){
          if (dirs.Contains(FROM_DIRECTIONS.BOTTOM)) {
            if (times_rotated_cw == 0 || times_rotated_cw == 3) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.LEFT)) {
            if (times_rotated_cw == 1 || times_rotated_cw == 0) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.TOP)) {
            if (times_rotated_cw == 2 || times_rotated_cw == 1) {
              return true;
            }
          }
          if (dirs.Contains(FROM_DIRECTIONS.RIGHT)) {
            if (times_rotated_cw == 3 || times_rotated_cw == 2) {
              return true;
            }
          }
        }
        return false;
     }


    bool check_color(int x_index, int y_index){
      string coord = x_index.ToString() + "," + y_index.ToString();
      if((gameObject.name.Contains("Blue") && (globals.board_colors[coord] != COLORS.BLUE || intermittent_interrupt.current_disabled == COLORS.BLUE)) ||
      (gameObject.name.Contains("Orange") && (globals.board_colors[coord] != COLORS.ORANGE || intermittent_interrupt.current_disabled == COLORS.ORANGE)) ||
      (gameObject.name.Contains("Yellow") && (globals.board_colors[coord] != COLORS.YELLOW || intermittent_interrupt.current_disabled == COLORS.YELLOW)) ||
      (gameObject.name.Contains("Green") && (globals.board_colors[coord] != COLORS.GREEN || intermittent_interrupt.current_disabled == COLORS.GREEN))){;
        return false;
      }
      return true;
    }

    void update_stock() {
      stock--;
      globals.piece_color_counts[color]--;
      globals.finishedPieces++;
      Debug.Log(globals.finishedPieces);
      text.text = stock.ToString();
      if (stock == 0) {
        //globals.finishedPieces++;
        if (globals.finishedPieces == globals.numPieces) {
          globals.gameLost = true;
        }
        Destroy(text);
        Destroy(gameObject);
      }
    }



    void update_victory() {
      foreach (string key in globals.reachable_moves.Keys){
            if (int.Parse(key.Substring(0,key.IndexOf(','))) >= globals.BOARD_WIDTH) {
                globals.gameWon = true;
                break;
            }
      }
    }

 }
