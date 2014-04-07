using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour{

	int fontSize;
	void Start(){
		fontSize = guiText.fontSize;
		}

	void Update(){
		if(CharacterSelection.currentPlayer <= 4)
			guiText.text = "Player " + CharacterSelection.currentPlayer + " choose";
		else
			guiText.text = "All characters locked in";
		}

}
