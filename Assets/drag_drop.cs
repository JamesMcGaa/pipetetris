using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_drop : MonoBehaviour
 {
     private Vector3 screenPoint;
     private Vector3 offset;
     private Vector3 original_loc;
     private bool pressed = false;
     private int times_rotated_ccw = 0;

     private float X_BOTTOM_LEFT_CORNER = -7.5f;
     private float Y_BOTTOM_LEFT_CORNER = -5.5f;

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    // This script will simply instantiate the Prefab when the game starts.

    //https://www.colorcombos.com/color-schemes/2/ColorCombo2.html

    void Start(){
        original_loc = gameObject.transform.position;

    }

     void Update()
     {
         if((Input.GetMouseButtonDown(1) || Input.GetKeyUp("space")) && pressed){ //right click
            transform.Rotate(0,0,-90); //Counterclockwise
            times_rotated_ccw = (times_rotated_ccw + 1) % 4;
         }
     }

     void OnSpaceDown() {

     }

     void OnMouseDown() {
         //start dragging the object, saving its offset
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

         if(globals.reachable_moves.ContainsKey(x_index.ToString() + "," + y_index.ToString())
         && x_index >= 0 && y_index >= 0 && x_index < globals.BOARD_WIDTH && y_index < globals.BOARD_HEIGHT 
         && check_orientation(x_index, y_index, times_rotated_ccw)
         ){
            //instantiate a new object
            curPosition.x = Mathf.Round(curPosition.x - .5f) + .5f;
            curPosition.y = Mathf.Round(curPosition.y - .5f) + .5f;
            Instantiate(myPrefab, curPosition, gameObject.transform.rotation);

            //TODO: update reachable
         }

        //reset state
        gameObject.transform.rotation = Quaternion.identity;
        times_rotated_ccw = 0;
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

     bool check_orientation(int x_index, int y_index, int times_rotated_ccw){
        if (gameObject.name.Contains("Straight")){

        }

        else if (gameObject.name.Contains("Split")){

        }

        else{ //Turn

        }
        
        return true;
     }
     
 }
