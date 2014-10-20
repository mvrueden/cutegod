using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	public WorldGenerationScript worldGeneration;

	public GameObject selector;

	private bool selection;
	
	private GameObject pickedUp;

	public float dropHeight = 2f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown(1)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);		
			if (hit.collider != null) {

				GameObject hitObject = hit.collider.gameObject;
				Tile hitTile = hitObject.GetComponent<Tile>();
				Vector3 hitPosition = hitTile.position;

				if (Input.GetMouseButton(0)) {
					// enable the collider in the first column in that stack

					//hitObject.collider2D.enabled = true;
					hitObject.renderer.material.color = Color.red;

					GameObject lower = worldGeneration.getTile(new Vector3(hitPosition.x, hitPosition.y-1, hitPosition.z));
					lower.layer = LayerMask.NameToLayer("Ground");					

					// drop a new element on top of the current stack
					GameObject newObject = worldGeneration.createTile (TileType.Dirt, (int)hitPosition.x, (int)hitPosition.y, (int)hitPosition.z+1);
					newObject.transform.position = new Vector3(newObject.transform.position.x, newObject.transform.position.y + dropHeight, newObject.transform.position.z);
					newObject.layer = LayerMask.NameToLayer("Float");
					//newObject.collider2D.enabled = true;
					newObject.rigidbody2D.isKinematic = false; // otherwise it will not fall

					Debug.Log ("New Block created: x=" + newObject.GetComponent<Tile>().position.x + ",y=" + newObject.GetComponent<Tile>().position.y + ",z=" + newObject.GetComponent<Tile>().position.z);				
					Debug.Log (newObject.layer);
					Debug.Log (hitObject.layer);
					//Debug.Log ("====");
				}

				if (Input.GetMouseButton (1)) {	
					if (!hitTile.isGround()) {
						Destroy (hitObject);
						GameObject gameObject = worldGeneration.getTile(new Vector3(hitPosition.x, hitPosition.y, hitPosition.z-1));
                		gameObject.collider2D.enabled = true;
					}
				}
			}
		}	
	}
}
