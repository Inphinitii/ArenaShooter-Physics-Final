using UnityEngine;
using UnityEditor;
using System.Collections;

public class TiledImporter : EditorWindow {

	TextAsset myTiledXML;
	TileMap myTileMap;
	[MenuItem("Window/Tiled Importer")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow (typeof(TiledImporter));
	}

	void OnGUI()
	{
		GUILayout.Label ("Tiled Importer", EditorStyles.boldLabel);
		myTiledXML = (TextAsset)EditorGUILayout.ObjectField(myTiledXML, typeof(TextAsset), false) as TextAsset;
		TileMap.pixelPerUnit = EditorGUILayout.IntField("Pixels Per Unit", TileMap.pixelPerUnit);
		if (GUILayout.Button ("Build Map") && myTiledXML != null) 
		{
			Build ();
			Debug.Log("Building Level..");
		}
	}

	void Build()
	{
		myTileMap = new TileMap (myTiledXML);
	}
}
