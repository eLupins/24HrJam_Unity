using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textboxTrig : MonoBehaviour {
    [SerializeField]
    Canvas messageCanvas; //Make the object in the script

    // Use this for initialization
    void Start()
    {
        messageCanvas.enabled = false; //this makes the canvas disappear
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        { //if a gameobject by the name of "Player" touches the collider
            TurnOnMessage(); //call the TurnOnMessage() function
        }
    }

    private void TurnOnMessage()
    {
        messageCanvas.enabled = true; //make the canvas appear
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        { //if a gameobject by the name of Vinny is NOT touching the collider
            TurnOffMessage(); //call the TurnOffMessage() function
        }
    }
    private void TurnOffMessage()
    {
        messageCanvas.enabled = false; //make the canvas disappear
    }
}