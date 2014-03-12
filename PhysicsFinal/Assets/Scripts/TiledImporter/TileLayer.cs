using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
//------------------------------------ Author: Tyler Remazki ----------------------------------\\
//------TileLayer.cs handles the generation of a tilemap with data given from TileMap.cs-------\\
//-----Differs from the reference in that it uses individual objects, not a singular mesh------\\
//-----This is less efficient, but I want to get this working my way before learning more------\\

//Inspired + Coded with reference from UNITMX
//https://bitbucket.org/Chaoseiro/x-unitmx

public class TileLayer{
	TileSet layerTileSet;
	string[] layerData;
	string layerName;
	int layerWidth;
	int layerHeight;
	int currentLayer;

	SpriteRenderer myRenderer;
	Material defaultMaterial;
	Texture2D myTexture;
	string myTextureLocation;

	public TileLayer(string _layerName, string _layerData, int _layerWidth, int _layerHeight, TileSet _myTileSet, int _currentLayer)
	{
		layerName = _layerName;
		layerData = _layerData.Split(',');
		layerWidth = _layerWidth;
		layerHeight = _layerHeight;
		layerTileSet = _myTileSet;
		currentLayer = _currentLayer;
		myTextureLocation = _myTileSet.ImageSource.TrimStart('.');
	}

	public string LayerName
	{
		get {return layerName;}
	}

	public string TexturePath
	{
		get {return myTextureLocation;}
	}

	public string[] LayerData
	{
		get { return layerData;}
	}

	public int LayerWidth
	{
		get { return layerWidth; }
	}

	public int LayerHeight
	{
		get { return layerHeight;}
	}

	public TileSet TileSet
	{
		get { return layerTileSet;}
	}
}
