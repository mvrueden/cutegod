using UnityEngine;
using System.Collections;

public class LeftPaneScript : MonoBehaviour {

	public Sprite scoreSprite;

	public float scoreSpriteScale = 1f;

	public float width = 200; 

	public float alpha = 0.7f;

	public GameObject game;

	private float height;
	

	// Use this for initialization
	void Start () {
		height = Camera.main.pixelHeight;
	}

	void OnGUI() {
		// the background
		Texture2D texture = new Texture2D(1, 1);
		Color color = new Color (0, 0, 0, alpha);
		texture.SetPixel(0, 0, color);
		texture.wrapMode = TextureWrapMode.Repeat;
		texture.Apply();
		GUI.DrawTexture(new Rect(0, 0, width, height), texture);

		// The Score Icon
		GUI.DrawTexture (new Rect (0, height - 200, scoreSprite.texture.width, scoreSprite.texture.height), scoreSprite.texture);

		// The Score Text
		GUI.Label (new Rect (100, height - 120, 50, 50), "SCORE:");



		//GUI.DrawTexture(new Rect(10, 10, 60, 60), aTexture, ScaleMode.ScaleToFit, true, 10.0F);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
