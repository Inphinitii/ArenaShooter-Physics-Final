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
	TileLayer myTileLayer;


	int x;
	int y;
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

				//Create a "root" for the layers. If it's a collision layer, ignore this step
				GameObject rootLayer;
				if(layerName != "Collision")
					rootLayer = new GameObject(layerName);
				else
					rootLayer = null;

				myTileLayer = new TileLayer(layerName, dataText, layerWidth, layerHeight, myTileSet, currentLayer, rootLayer);

				if(outerNode.FirstChild.Name == "properties")
				{
					//Get the "property" node of the "proeprties" node and take the inner text of the attribute value
					//This value corresponds to what layer this particular collision layer corresponds to
					string colliderFor = outerNode.FirstChild.FirstChild.Attributes["value"].InnerText;
					myTileLayer.ColliderFor = colliderFor;
				}

				//Create a new TileLayer object and add it to the list
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
		foreach(TileLayer layer in myTileLayers)
		{
			/* -- GENERATE THE ART LAYER -- */
			if(layer.LayerName != "Collision")
			{
				//Generate the sprite list..
				CreateSpriteList (layer);
				//Generate the layer...
				GenerateLayer (layer);
			}

			/* -- GENERATE THE COLLISION LAYER -- */
			else if(layer.LayerName == "Collision" || layer.LayerName == "collision")
			{
				GenerateCollisionLayer(layer);
			}

		}
	}

	//SPRITE SHEET
	private void CreateSpriteList(TileLayer layer)
	{
		Texture2D myTexture = (Texture2D)Resources.Load(layer.TexturePath.Trim('/').Split('.')[0]) as Texture2D;
		int amountOfTiles = myTexture.width/layer.TileSet.TileWidth * myTexture.height/layer.TileSet.TileHeight;
		x = 0;
		y = 0;
		
		//Separate the texture into multiple sprites for ease of access, as Unity reads texture coordinates from the bottom left.
		for(int i = 0 ; i < amountOfTiles; i++)
		{
			if(x > 0 && x == myTexture.width/layer.TileSet.TileWidth)
			{
				x = 0;
				y++;
			}
			
			mySprite = Sprite.Create(myTexture, new Rect(32*x, myTexture.height - 32*(y+1), layer.TileSet.TileWidth, layer.TileSet.TileHeight), 
			                         new Vector2(0,0), layer.TileSet.TileWidth);
			
			//Add the clipped sprite to the sprite list
			mySpriteList.Add(mySprite);
			x++;
		}
	}

	//ART LAYER
	private void GenerateLayer(TileLayer myLayer)
	{
		Material defaultMaterial = (Material)Resources.Load("Materials/Sprites") as Material; //Get the default sprite material
		x = 0;
	    y = 0;
		int z = myLayer.CurrentLayer;

		for(int i = 0; i < myLayer.LayerWidth * myLayer.LayerHeight; i++)
		{
			int index = int.Parse(myLayer.LayerData[i]);

			if(x > 0 && x >= myLayer.LayerWidth)
			{
				x = 0;
				y--;
			}
			if(index > 0)
			{
				GameObject tileObject = new GameObject("Tile");
				SpriteRenderer myRenderer = tileObject.AddComponent<SpriteRenderer>();
				
				//Set the position of the GameObjects
				tileObject.transform.parent = myLayer.GetRoot.transform;
				tileObject.transform.position = new Vector3(x,y,z);
				
				//Set the material and sprite on the GameObject for rendering purposes
				myRenderer.material = defaultMaterial;
				myRenderer.sprite = mySpriteList[index-1];
			}
			x++;
		}
	}

	//COLLISION LAYER
	private void GenerateCollisionLayer(TileLayer layer)
	{
		x = 0;
		y = 0;
		GameObject rootLayer = null;

		for(int i = 0; i < myTileLayers.Count; i++)
		{
			if(layer.ColliderFor == myTileLayers[i].LayerName)
			{
				rootLayer = myTileLayers[i].GetRoot;
			}
		}

		//Generate the layer...
		for(int i = 0; i < layer.LayerWidth * layer.LayerHeight; i++)
		{
			int index = int.Parse (layer.LayerData[i]);
			if(x > 0 && x == layer.LayerWidth)
			{
				x = 0;
				y--;
			}

			if(index != 0 )
			{
				GameObject collisionObject = new GameObject("Collider");
				collisionObject.AddComponent<BoxCollider2D>();
				collisionObject.GetComponent<BoxCollider2D>().center = new Vector2(0.5f, 0.5f);
				collisionObject.transform.parent = rootLayer.transform;
				collisionObject.transform.position = new Vector3(x,y,0);
			}
			x++;
		}
	}
}
