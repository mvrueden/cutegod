using UnityEngine;
using System.Collections;

public class ControlsScript : MonoBehaviour {

	private bool isPanning;
	private Vector3 mouseOrigin;
	private bool isMenuShown;
	public float panSpeed = 4.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		float yAxisValue = Input.GetAxis("Vertical");
		if (Camera.main != null) {
			Camera.main.transform.Translate (new Vector2 (xAxisValue * 0.2f, yAxisValue * 0.2f));
		}

		// Get the right mouse button
		if(Input.GetMouseButtonDown(1)) {
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isPanning=false;

		// Move the camera on it's XY plane
		if (isPanning) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);			
			Vector3 move = new Vector2(pos.x * panSpeed, pos.y * panSpeed);
			Camera.main.transform.Translate(move, Space.Self);
		}	

		// Click logic

	}

	void OnGUI() {
		if (Input.GetKeyDown (KeyCode.Escape) || isMenuShown) {
			isMenuShown = true;
			if (isMenuShown) {
				GUI.Window (1, new Rect(Camera.main.pixelWidth / 2 - 100, Camera.main.pixelHeight / 2 - 100, 200, 200), GameWindowFunction, "Game Menu"); 	
			}
		}
		
		Vector3 position = Input.mousePosition;
		GUI.Label (new Rect (800, 0, 100, 50), "x=" + position.x + ",y=" + position.y);
	}

	void GameWindowFunction (int windowID) {
		Time.timeScale = 0;
		// Draw any Controls inside the window here
		if (GUI.Button (new Rect (0, 20, 100, 20), "Resume")) {
			isMenuShown = false;
			Time.timeScale = 1;
		}
		if (GUI.Button (new Rect (0, 50, 100, 20), "Exit")) {
			Application.CancelQuit();
		}
	}
}
