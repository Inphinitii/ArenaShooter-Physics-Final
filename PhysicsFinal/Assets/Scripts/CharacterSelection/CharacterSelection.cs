using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {
	
	public int p_numberOfCharacters;
	public float p_spacingBetweenPortraits;
	public float startingX;

	public Portrait[] spriteArray;

	private Portrait[] internalArray;
	private int currentlySelected;
	private bool wait;

	public static int currentPlayer;
	// Use this for initialization
	void Start () {
		currentPlayer = 1;
		float x = startingX;
		float y = -0.5f;
		internalArray = new Portrait[p_numberOfCharacters];

		//Space out the portraits evenly and instantiate them.
		for (int i = 0; i < p_numberOfCharacters; i++) 
		{
			internalArray[i] = Instantiate(spriteArray[i], new Vector3(x + (p_spacingBetweenPortraits * i), y, -5.0f), Quaternion.identity) as Portrait;
		}

		//ORDER IN ARRAY OF CHARACTER PORTRAITS 
		/* 0 - BLUE
		 * 1 - GREEN
		 * 2 - RED
		 * 3 - YELLOW */

		//Set the first element to be selected
		currentlySelected = 0;
		wait = false;
	}
	
	// Update is called once per frame
	void Update () {
		SetSelected (currentlySelected);

		if (!wait)
			StartCoroutine (HandleInput(0.05f));
	}

	public IEnumerator HandleInput(float _wait) { 
		wait = true;
		yield return new WaitForSeconds (_wait);
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			currentlySelected++;
		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			currentlySelected--;
		}

		if (currentPlayer != internalArray.Length + 1) {
			if (Input.GetKey (KeyCode.Space)) {
					internalArray [currentlySelected].p_selected = true;
					if (currentPlayer == 1) {
							God.player1Character = (God.CharacterEnum)currentlySelected;
							Debug.Log(God.player1Character);
					}
					if (currentPlayer == 2) {
							God.player2Character = (God.CharacterEnum)currentlySelected;
							Debug.Log(God.player2Character);
					}
					if (currentPlayer == 3) {
						God.player3Character = (God.CharacterEnum)currentlySelected;
						Debug.Log(God.player3Character);
					}
					if (currentPlayer == 4) {
						God.player4Character = (God.CharacterEnum)currentlySelected;
						Debug.Log(God.player4Character);
					}
					currentPlayer++;

				
			}
		}

		/*if (Input.GetKey (KeyCode.KeypadEnter)) {
			Application.LoadLevel(0);
		}*/
		
		if (currentlySelected >= internalArray.Length) {
			currentlySelected = 0;
		} else if (currentlySelected < 0) {
			currentlySelected = internalArray.Length -1;
		}

		wait = false;

	}
	void SetSelected(int index){
		for (int i = 0; i < internalArray.Length; i++) {
			if(i == index) {
				if(internalArray[i].p_selected)
					currentlySelected++;
				else
						internalArray[i].p_hovered = true;
			}
			else { internalArray[i].p_hovered = false; }
		}
	}
}
