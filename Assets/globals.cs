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

public enum DIFFICULTY
{
    EASY,
    MEDIUM,
    HARD,
    INSANE,
    EXTREME
}

public class globals : MonoBehaviour
{
    public static DIFFICULTY difficulty_level = DIFFICULTY.EASY;
    public static bool gameStarted = false;
    public static bool gameLoaded = false;
    public static bool gameWon = false;
    public static bool gameLost = false;
    public static int finishedPieces = 0;
    public static int numPieces = 0;
    public static int turnsTaken = 0;
    public static int timesDisabled = 0;
    public static Dictionary<string, int> piece_stocks = new Dictionary<string, int>();
    public static Dictionary<string, COLORS> board_colors = new Dictionary<string, COLORS>();
    public static Dictionary<string, Object> color_squares = new Dictionary<string, Object>();
    public static Dictionary<string, List<FROM_DIRECTIONS>> reachable_moves = new Dictionary<string, List<FROM_DIRECTIONS>>();
    public static Dictionary<string, int> occupied_squares = new Dictionary<string, int>();
    public static Dictionary<COLORS, int> piece_color_counts = new Dictionary<COLORS, int>
    {
      {COLORS.BLUE, 0},
      {COLORS.GREEN, 0},
      {COLORS.ORANGE, 0},
      {COLORS.YELLOW, 0}
    };
    // public static Dictionary<string, string> board = new Dictionary<string, string>();

    public const int BOARD_WIDTH = 16;
    public const int BOARD_HEIGHT = 12;

    public GameObject orange;
    public GameObject yellow;
    public GameObject green;
    public GameObject blue;
    public Vector3 victoryPos = new Vector3(0, 0, 0);
    public GameObject victory;
    public Vector3 defeatPos = new Vector3(0, 0, 0);
    public GameObject defeat;


    void Update() {
        // Debug.Log(numPieces);
        if (gameStarted && !gameLoaded) {
          gameLoaded = true;
          for (int i = 0; i < BOARD_HEIGHT; i++)
          {
              List<FROM_DIRECTIONS> list = new List<FROM_DIRECTIONS>();
              list.Add(FROM_DIRECTIONS.LEFT);
              reachable_moves.Add("0," + i.ToString(), list); // left column is accessible
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
        if (gameWon) {
          Instantiate(victory, victoryPos, Quaternion.identity);
          enabled = false;
          return;
        } else if (gameLost) {
          Instantiate(defeat, defeatPos, Quaternion.identity);
          enabled = false;
        }
    }
}
