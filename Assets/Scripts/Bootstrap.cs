using UnityEngine;
using System.Collections;

/**
 * This is a helper class to bootstrap the "main menu"
 */
public class Bootstrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Start Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,100,80,20), "Exit")) {
			Application.Quit();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
