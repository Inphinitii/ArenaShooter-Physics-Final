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
	string colliderFor;
	int layerWidth;
	int layerHeight;
	int currentLayer;
	GameObject rootObject;
	string myTextureLocation;

	public TileLayer(string _layerName, string _layerData, int _layerWidth, int _layerHeight, TileSet _myTileSet, int _currentLayer, GameObject _root)
	{
		layerName = _layerName.Trim();
		layerData = _layerData.Split(',');
		layerWidth = _layerWidth;
		layerHeight = _layerHeight;
		layerTileSet = _myTileSet;
		currentLayer = _currentLayer;
		myTextureLocation = _myTileSet.ImageSource.TrimStart('.');
		rootObject = _root;
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

	public int CurrentLayer
	{
		get {return currentLayer;}
	}

	public TileSet TileSet
	{
		get { return layerTileSet;}
	}

	public string ColliderFor
	{
		get { return colliderFor; }
		set { colliderFor = value; }
	}

	public GameObject GetRoot
	{
		get { return rootObject; }
	}

}
