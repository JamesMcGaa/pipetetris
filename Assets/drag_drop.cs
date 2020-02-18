using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_drop : MonoBehaviour
 {
     private Vector3 screenPoint;
     private Vector3 offset;
     private Vector3 original_loc;
     private bool pressed = false;

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
         if(Input.GetMouseButtonDown(1) && pressed){ //right click
            transform.Rotate(0,0,90);
         }
     }

     void OnMouseDown() {
        pressed = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
     }

     void OnMouseUp() {
         pressed = false;
         transform.position = original_loc;
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;

         int x_index = (int)Mathf.Round(curPosition.x - X_BOTTOM_LEFT_CORNER);
         int y_index = (int)Mathf.Round(curPosition.y - Y_BOTTOM_LEFT_CORNER);
         if(y_index != 0){
             gameObject.transform.rotation = Quaternion.identity;
             return;
         }

         curPosition.x = Mathf.Round(curPosition.x - .5f) + .5f;
         curPosition.y = Mathf.Round(curPosition.y - .5f) + .5f;
         Instantiate(myPrefab, curPosition, gameObject.transform.rotation);
         gameObject.transform.rotation = Quaternion.identity;
     }
     
     void OnMouseDrag()
     {
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
         curPosition.x = Mathf.Round(curPosition.x - .5f) + .5f;
         curPosition.y = Mathf.Round(curPosition.y - .5f) + .5f;
         transform.position = curPosition;
     }
     
 }
