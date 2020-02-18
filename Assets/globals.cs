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

public class globals : MonoBehaviour
{
    public static bool gameStarted = false;
    public static Dictionary<string, List<FROM_DIRECTIONS>> reachable_moves = new Dictionary<string, List<FROM_DIRECTIONS>>();
    public static Dictionary<string, int> occupied_squares = new Dictionary<string, int>();
    // public static Dictionary<string, string> board = new Dictionary<string, string>();

    public const int BOARD_WIDTH = 16;
    public const int BOARD_HEIGHT = 12;

    void Awake(){
        for (int i = 0; i < BOARD_WIDTH; i++)
        {
            List<FROM_DIRECTIONS> list = new List<FROM_DIRECTIONS>();
            list.Add(FROM_DIRECTIONS.BOTTOM);
            reachable_moves.Add(i.ToString() + ",0", list); //bottom row is accessible
        }
    }
}
