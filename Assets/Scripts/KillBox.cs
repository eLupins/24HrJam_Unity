using UnityEngine;
using System.Collections;

//this script is to make sure the player dies if they fall off the edge
public class KillBox : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player"){ //so if you touch a gameobject whose tag is Player
			Destroy (gameObject); //destroy the gameobject

            Debug.Log("Kill Box Notice: Entity died."); //print to the console to make sure this script is sorta working
		
		}
        if (col.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }
	}

}
