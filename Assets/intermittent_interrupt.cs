using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intermittent_interrupt : MonoBehaviour
{

public static COLORS current_disabled;

public Vector3 curPosition = new Vector3(9.5f ,3.5f, 0);
public GameObject orange;
public GameObject yellow;
public GameObject green;
public GameObject blue;
public GameObject disable;

public GameObject current = null;
private bool invoked = false;
 void Update () {
     if(!invoked && globals.gameStarted){
        InvokeRepeating("DisableRandom", 0f, 3f);  //0s delay, repeat every 10s
        Instantiate(disable, curPosition, Quaternion.identity);
        invoked = true;
     }
 }

 void DisableRandom() {
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
