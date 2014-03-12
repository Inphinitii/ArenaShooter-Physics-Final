using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

//------------------------------------ Author: Tyler Remazki ----------------------------------\\
//-----TileMap.cs handles the construction of a tilemap that has been exported from Tiled------\\
//----------in conjunction with two other classes that allow for organization of data----------\\

//Inspired + Coded with reference from UNITMX
//https://bitbucket.org/Chaoseiro/x-unitmx


public class TileMap {

	TextAsset myTextAsset;
	TileSet myTileSet;
	List<TileLayer> myTileLayers;
	List<Sprite> mySpriteList;
	Sprite mySprite;

	public TileMap(TextAsset _text)
	{
		myTextAsset = _text;
		Start ();
	}

	/*-- Generate your level based on the XML file passed above --*/
	void Start()
	{
		myTileSet = null;
		myTileLayers = new List<TileLayer>();
		mySpriteList = new List<Sprite> ();
		int currentLayer = 0; //The current layer being generated - Used to help draw Z displacement 

		/*-- LOAD THE XML FILE --*/
		XmlDocument myXML = new XmlDocument();
		myXML.Load (new System.IO.StringReader(myTextAsset.text));
		XmlNodeList myNodeList = myXML.DocumentElement.ChildNodes;

		foreach(XmlNode outerNode in myNodeList)
		{
			switch(outerNode.Name)
			{
			/*-- TILE SET -- */
			case "tileset":

				//Tile data
				string tileSetName = outerNode.Attributes["name"].InnerText; 
				int firstGid = int.Parse(outerNode.Attributes["firstgid"].InnerText); 
				int tileWidth = int.Parse(outerNode.Attributes["tilewidth"].InnerText);
				int tileHeight = int.Parse(outerNode.Attributes["tileheight"].InnerText); 

				//Image data
				string imageSource = outerNode.FirstChild.Attributes["source"].InnerText; 
				int imageWidth = int.Parse (outerNode.FirstChild.Attributes["width"].InnerText);
				int imageHeight = int.Parse (outerNode.FirstChild.Attributes["height"].InnerText);

				//Create a new Tileset object for use in the layer
			    myTileSet = new TileSet(imageSource, tileSetName, firstGid, tileWidth, tileHeight, imageWidth, imageHeight);
					break;

			/*-- LAYER -- */
			case "layer": 

				//Layer Data
				XmlNode myDataNode = outerNode.SelectSingleNode("data");
				int layerWidth = int.Parse(outerNode.Attributes["width"].InnerText);
				int layerHeight = int.Parse(outerNode.Attributes["height"].InnerText); 
				string layerName = outerNode.Attributes["name"].InnerText;
				string dataText = myDataNode.InnerText;

				//Create a new TileLayer object
				TileLayer myTileLayer = new TileLayer(layerName,dataText, layerWidth, layerHeight, myTileSet, currentLayer);
				myTileLayers.Add(myTileLayer);
				currentLayer++;
					break;
			}
		}
		CreateLevelPrefab();
	}

	/* CREATES THE LEVEL */
	void CreateLevelPrefab()
	{
		Material defaultMaterial = (Material)Resources.Load("Materials/Sprites") as Material; //Get the default sprite material

		foreach(TileLayer layer in myTileLayers)
		{
			if(layer.LayerName != "Collision")
			{
				//Create a root gameobject for the layer to be generated underneath
				GameObject rootLayer = new GameObject(layer.LayerName);

				//Load the texture file that was passed to the layer - Assuming it's in the resources folder of the project
				Texture2D myTexture = (Texture2D)Resources.Load(layer.TexturePath.Trim('/').Split('.')[0]) as Texture2D;

				//Variables..
				int amountOfTiles = myTexture.width/layer.TileSet.TileWidth * myTexture.height/layer.TileSet.TileHeight;
				int x = 0;
				int y = 0;

				//Separate the texture into multiple sprites for ease of access, as Unity reads texture coordinates from the bottom left.
				for(int i = 0 ; i < amountOfTiles; i++)
				{
					if(x > 0 && x == myTexture.width/layer.TileSet.TileWidth)
					{
						x = 0;
						y++;
					}

					mySprite = Sprite.Create(myTexture, new Rect(32*x, myTexture.height - 32*(y+1), layer.TileSet.TileWidth, layer.TileSet.TileHeight), new Vector2(0,0), 32f);
					mySpriteList.Add(mySprite);
					x++;
				}

				x = 0;
				y = 0;
				int spriteIndex;

				//Generate the layer...
				for(int i = 0; i < layer.LayerWidth * layer.LayerHeight; i++)
				{
					if(x > 0 && x == layer.LayerWidth)
					{
						x = 0;
						y--;
					}
					GameObject tileObject = new GameObject("Tile");
					SpriteRenderer myRenderer = tileObject.AddComponent<SpriteRenderer>();

					//Set the position of the GameObjects
					tileObject.transform.position = new Vector3(x,y,0);

					//Set the parent to the root object
					tileObject.transform.parent = rootLayer.transform;

					//Set the material and sprite on the GameObject for rendering purposes
					myRenderer.material = defaultMaterial;
					spriteIndex = int.Parse(layer.LayerData[i])-1;

					if(spriteIndex >= 0)
						myRenderer.sprite = mySpriteList[spriteIndex];

					x++;
				}
				UnityEditor.PrefabUtility.CreatePrefab("Assets/Prefabs/newPrefab.prefab", rootLayer);
			}
		}
	}
}
