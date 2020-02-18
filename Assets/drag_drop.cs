using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_drop : MonoBehaviour
 {
     private Vector3 screenPoint;
     private Vector3 offset;
     private Vector3 original_loc;
     private bool pressed = false;


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
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
         curPosition.x = Mathf.Round(curPosition.x);
         curPosition.y = Mathf.Round(curPosition.y);
         Instantiate(myPrefab, curPosition, gameObject.transform.rotation);
         transform.position = original_loc;
         gameObject.transform.rotation = Quaternion.identity;
     }
     
     void OnMouseDrag()
     {
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint)+offset;
         curPosition.x = Mathf.Round(curPosition.x);
         curPosition.y = Mathf.Round(curPosition.y);
         transform.position = curPosition;
     }
     
 }
