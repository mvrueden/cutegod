using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector3 position;

	public TileType tileType;

	public bool isGround() {
		return TileType.Ground == tileType;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Debug.Log ("Enter");		
	}
	
	
	void OnCollisionStay2D(Collision2D coll) {
		if (new Vector2 (0, 0) == coll.relativeVelocity) {
			Debug.Log ("Force: " + coll.relativeVelocity);
			coll.rigidbody.isKinematic = true;
		}
		//coll.gameObject.rigidbody2D.isKinematic = true;
		//coll.gameObject.renderer.material.color = Color.black;
		
	}

	void OnCollisionExit2D(Collision2D coll) {
		Debug.Log ("Exit");
	}


	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log ("wiuwiu");
	}
}
