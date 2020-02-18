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
    public static Dictionary<string, FROM_DIRECTIONS> reachable_moves = new Dictionary<string, FROM_DIRECTIONS>();
    public static Dictionary<string, string> board = new Dictionary<string, string>();

    public const int BOARD_WIDTH = 16;
    public const int BOARD_HEIGHT = 12;

    void Awake(){
        for (int i = 0; i < BOARD_WIDTH; i++)
        {
            reachable_moves.Add(i.ToString() + ",0", FROM_DIRECTIONS.BOTTOM); //bottom row is accessible
        }
    }
}
