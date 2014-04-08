using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour{

	int fontSize;
	void Start(){

		}

	void Update(){
		if(CharacterSelection.currentPlayer <= 4)
			//guiText.text = "Player " + CharacterSelection.currentPlayer + " choose";
			GetComponent<TextMesh>().text = "Player " + CharacterSelection.currentPlayer + " choose";
		else
			GetComponent<TextMesh>().text = "All characters locked in";
		}

}
