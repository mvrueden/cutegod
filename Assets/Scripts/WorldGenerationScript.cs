using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerationScript : MonoBehaviour {

	public GameObject grassTile;
	public GameObject waterTile;
	public GameObject groundTile;
	public GameObject stoneTile;
	public GameObject dirtTile;
	
	public GameObject floraTiles;
	public GameObject villagers;

	public int width = 10;
	public int height = 10;
	public int maxDepth = 5;

	public float floraPercentuage = 0.1f;
	public float yOffset = 0.8f;
	public float depthOffset = 0.4f;
	
	public TileType[] basicTiles = new TileType[]{TileType.Water, TileType.Dirt, TileType.Grass, TileType.Stone};

	private int numberTiles = 0;
	
	private Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>> tiles = new Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>>();

	// Use this for initialization
	void Start () {
		print ("started");

		TileType[,,] worldArray = GenerateTerrain ();

		PlantTrees (worldArray);
		Populate (worldArray);

		Visualize (worldArray);

		print ("world created");
	}

	private TileType[,,] GenerateTerrain() {
		TileType[,,] worldArray = new TileType[width, height, maxDepth + 2];

		// generate World
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) { 
				worldArray[x,y,0] = TileType.Ground;
				numberTiles++;

				/*
				int depth = Random.Range (1, maxDepth);
				for (int z=0; z < depth + 2; z++) {
					if (z == 0) {
						worldArray[x,y,z] = TileType.Ground;
					} else {
						TileType randomTileType = basicTiles[Random.Range (0, basicTiles.Length)];
						worldArray[x,y,z] = randomTileType;
					}
					numberTiles++;
				}*/
			}
		}	
		return worldArray;
	}

	void PlantTrees(TileType[,,] worldArray) {
		// generate Flora
		//int floraTileNumber = (int) ((numberTiles - width * height) * floraPercentuage); // substract base line
		//for (int i=0; i<floraTileNumber; i++) {
		//	int floraTileId = Random.Range (0, floraTiles.transform.childCount);
		//
		//}
	}

	void Populate(TileType[,,] worldArray) {

	}

	void Visualize(TileType[,,] worldArray) {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				for (int z = 0; z < maxDepth + 2; z++) {
					TileType tileType = worldArray[x,y,z];
					if (tileType != TileType.Empty) {
						createTile (tileType, x, y, z);
					}				
				}
			}
		}
	}

	GameObject getTileForType(TileType tileType) {
		switch (tileType) {
			case TileType.Ground:
				return groundTile;
			case TileType.Dirt:
				return dirtTile;
			case TileType.Grass:
				return grassTile;
			case TileType.Stone:
				return stoneTile;
			case TileType.Water:
				return waterTile;
			case TileType.Empty:	
			default:
				throw new UnityException("blök");
		}
	}

	void addTile(GameObject gameObjectToAdd) {
		Tile tile = gameObjectToAdd.GetComponent<Tile> ();
		int x = (int) tile.position.x;
		int y = (int) tile.position.y;
		int z = (int) tile.position.z;

		if (!tiles.ContainsKey (x)) {
			tiles.Add (x, new Dictionary<int, Dictionary<int, GameObject>>());
		}
		if (!tiles[x].ContainsKey (y)) {
			tiles[x].Add (y, new Dictionary<int, GameObject>());
		}
		if (!tiles [x] [y].ContainsKey (z)) {
			tiles [x] [y].Add (z, gameObjectToAdd);
		} else {
			Debug.Log ("Element at position x="+x+",y="+y+",z="+z+" already exists.");
		}
	}

	/**
	 * The tiles are somewhere on the screen. 
	 * We now have to map the screen position to the game field position
	 * */
	public Vector3 WorldPointToGamePoint(GameObject gameObject) {
		return gameObject.GetComponent<Tile>().position;
	}

	public Vector2 GamePointToWorldPoint(Vector3 input) {
		return getPositionVector (input.x, input.y, input.z);
	}

	public Vector2 SelectorPosition(Vector3 input) {
		Vector2 result = GamePointToWorldPoint (input);
		result.y = result.y + depthOffset;
		return result;
	}

	public GameObject createTile(TileType tileType, int x, int y, int z) {
		GameObject rawTileObject = getTileForType(tileType);
		GameObject tileObject = clone (rawTileObject, x, y, z);
		Tile tile = tileObject.GetComponent<Tile>();
		tile.position = new Vector3(x,y,z);

		if (tileObject.collider2D != null) {
			//tileObject.collider2D.enabled = true;
		}
		if (tileObject.renderer != null) {
			tileObject.renderer.sortingOrder = z;
		}

		addTile (tileObject);
		return tileObject;
	}

	private GameObject clone(GameObject tileToClone, int x, int y, int z) {
		return (GameObject)Instantiate(tileToClone, getPositionVector(x,y,z), Quaternion.identity);
	}

	public GameObject getTile(Vector3 vector) {
		return tiles [(int)vector.x][(int)vector.y][(int)vector.z];
	}

	Vector3 getPositionVector(float x, float y, float z) {
		return new Vector3(x, y*yOffset + z*depthOffset, y);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
