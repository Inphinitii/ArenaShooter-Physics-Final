using UnityEngine;
using System.Collections;

public static class God{

	//This class holds variables that are necessary to carry over scenes.
	public enum CharacterEnum{
		Blue, 
		Green, 
		Red, 
		Yellow
	}
	public static CharacterEnum player1Character;
	public static CharacterEnum player2Character;
	public static CharacterEnum player3Character;
	public static CharacterEnum player4Character;
	
	public static Vector3 m_spawnLocation1 = new Vector3(-31, 13, -1);
	public static Vector3 m_spawnLocation2 = new Vector3(25, 13, -1);
	public static Vector3 m_spawnLocation3;
	public static Vector3 m_spawnLocation4;
	
	public static int playerCount = 0;
	
	
	public static GameObject Spawn(CharacterEnum m_Player){
		GameManager myManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
		Vector3 spawnLocation = Vector3.zero;
		Quaternion rotation;
		
		if(playerCount == 0){
			spawnLocation = m_spawnLocation1;	
			rotation = Quaternion.identity;	
		}
		else
		{
			spawnLocation = m_spawnLocation2;
			rotation = Quaternion.Euler(0, 180, 0);
		}
		
		playerCount++;
	
		
		switch(m_Player){
			case CharacterEnum.Blue:
				return MonoBehaviour.Instantiate(myManager.playerPrefabs[0], spawnLocation , rotation)as GameObject;
			break;
			
			case CharacterEnum.Green:
				return MonoBehaviour.Instantiate(myManager.playerPrefabs[1], spawnLocation , rotation) as GameObject;
			break;
			
			case CharacterEnum.Red:
				return MonoBehaviour.Instantiate(myManager.playerPrefabs[2], spawnLocation , rotation)as GameObject;			
			break;
			
			case CharacterEnum.Yellow:
				return MonoBehaviour.Instantiate(myManager.playerPrefabs[3], spawnLocation , rotation)as GameObject;			
			break;
			
		default:
			return new GameObject();
			break;
		}
	}
	
	
}
