using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//------------------------------------ Author: Tyler Remazki ----------------------------------\\
//-----TileSet.cs holds all of the data associated with the tilesets exported from Tiled-------\\
//--------allowing for easy access to tile width, height, and various other properties---------\\

//Inspired + Coded with reference from UNITMX
//https://bitbucket.org/Chaoseiro/x-unitmx

public class TileSet{
	
	private int tileWidth;
	private int tileHeight;
	private int imageWidth;
	private int imageHeight;
	private int firstGid;
	private string tileSetName;
	private string imgSource;

	public TileSet(string _imgSource, string _tileSetName, int _firstGid, int _tileWidth, int _tileHeight, int _imageWidth, int _imageHeight)
	{
		imgSource = _imgSource;
		tileSetName = _tileSetName;
		firstGid = _firstGid;
		tileWidth = _tileWidth;
		tileHeight = _tileHeight;
		imageWidth = _imageWidth;
		imageHeight = _imageHeight;
	}
	public int FirstGID
	{
		get { return firstGid; }
		set { firstGid = value; }
	}

	public int TileWidth
	{
		get { return tileWidth; }
		set { tileWidth = value; }
	}

	public int TileHeight 
	{
				get { return tileHeight; }
				set { tileHeight = value; }
	}

	public int ImageWidth
	{
		get { return imageWidth; }
		set { imageWidth = value; }
	}

	public int ImageHeight
	{
		get { return imageHeight; }
		set { imageHeight = value; }
	}

	public string ImageSource
	{
		get { return imgSource; }
		set { imgSource = value; }
	}

	public string TileSetName
	{
		get { return tileSetName; }
	}
}
