using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FROM_DIRECTIONS
{
    BOTTOM,
    LEFT,
    RIGHT,
    TOP
}

public enum COLORS
{
    ORANGE,
    YELLOW,
    GREEN,
    BLUE
}

public class globals : MonoBehaviour
{
    public static bool gameStarted = false;
    public static bool gameLoaded = false;
    public static bool gameWon = false;
    public static Dictionary<string, COLORS> board_colors = new Dictionary<string, COLORS>();
    public static Dictionary<string, Object> color_squares = new Dictionary<string, Object>();
    public static Dictionary<string, List<FROM_DIRECTIONS>> reachable_moves = new Dictionary<string, List<FROM_DIRECTIONS>>();
    public static Dictionary<string, int> occupied_squares = new Dictionary<string, int>();
    // public static Dictionary<string, string> board = new Dictionary<string, string>();

    public const int BOARD_WIDTH = 16;
    public const int BOARD_HEIGHT = 12;

    public GameObject orange;
    public GameObject yellow;
    public GameObject green;
    public GameObject blue;

    void Update(){
        if (gameStarted && !gameLoaded) {
          gameLoaded = true;
          enabled = false;
          for (int i = 0; i < BOARD_WIDTH; i++)
          {
              List<FROM_DIRECTIONS> list = new List<FROM_DIRECTIONS>();
              list.Add(FROM_DIRECTIONS.BOTTOM);
              reachable_moves.Add(i.ToString() + ",0", list); //bottom row is accessible
          }

          for (int i = 0; i < BOARD_WIDTH; i++)
          {
              for (int j = 0; j < BOARD_HEIGHT; j++)
              {
                  int x = Random.Range (0, 4);
                  Vector3 curPosition = new Vector3(drag_drop.X_BOTTOM_LEFT_CORNER + i ,drag_drop.Y_BOTTOM_LEFT_CORNER + j, 0);
                  string coord = i.ToString() + "," + j.ToString();
                  switch (x) {
                          case 0:
                            color_squares.Add(coord, Instantiate(orange, curPosition, Quaternion.identity));
                            board_colors.Add(coord, COLORS.ORANGE);
                            break;
                          case 1:
                            color_squares.Add(coord, Instantiate(yellow, curPosition, Quaternion.identity));
                            board_colors.Add(coord, COLORS.YELLOW);
                            break;
                          case 2:
                            color_squares.Add(coord, Instantiate(green, curPosition, Quaternion.identity));
                            board_colors.Add(coord, COLORS.GREEN);
                            break;
                          case 3:
                            color_squares.Add(coord, Instantiate(blue, curPosition, Quaternion.identity));
                            board_colors.Add(coord, COLORS.BLUE);
                            break;
                  }
              }
          }
        }
    }
}
