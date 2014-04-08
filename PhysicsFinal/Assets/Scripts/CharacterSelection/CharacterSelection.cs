using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {
	
	public int p_numberOfCharacters;
	public int p_numberOfPlayers;
	public float p_spacingBetweenPortraits;
	public float startingX;
	public GameObject pressStart;
	
	public Portrait[] spriteArray;
	private Portrait[] internalArray;
	private int currentlySelected;
	private bool wait;
	private bool movingRight;
	
	
	public static bool handleInput = false;
	public static int playerNumber;
	public static int currentPlayer;
	// Use this for initialization
	void Start () {
		currentPlayer = 1; //Player 1 gets first selection
		playerNumber = p_numberOfPlayers;
		internalArray = new Portrait[p_numberOfCharacters];

		//Space out the portraits evenly and instantiate them.
		for (int i = 0; i < p_numberOfCharacters; i++) 
		{
			internalArray[i] = Instantiate(spriteArray[i], new Vector3(startingX + (p_spacingBetweenPortraits * i), -0.2f, -5.0f), Quaternion.identity) as Portrait;
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
		if(handleInput){
			SetSelected (currentlySelected);
			PlayerSelection();
			
			if (!wait)
				StartCoroutine (HandleInput(0.1f));
			}
			
		if(currentPlayer == p_numberOfPlayers+1){
			pressStart.SetActive(true);
			CharacterSelection.handleInput = false;
		}
		
		PressStart();
	}

	public IEnumerator HandleInput(float _wait) { 
		wait = true;
		
		if (Input.GetAxisRaw ("Horizontal") > 0) 
		{
			currentlySelected++;
			movingRight = true;
		} 
		else if (Input.GetAxisRaw ("Horizontal") < 0) 
		{
			currentlySelected--;
			movingRight = false;
		}
		
		yield return new WaitForSeconds (_wait);

		/*if (Input.GetKey (KeyCode.KeypadEnter)) {
			Application.LoadLevel(0);
		}*/
		
		if (currentlySelected >= internalArray.Length) 
		{
			currentlySelected = 0;
		} 
		else if(currentlySelected < 0) 
		{
			currentlySelected = internalArray.Length -1;
		}

		wait = false;

	}
	
	void PlayerSelection(){
		if (currentPlayer != internalArray.Length + 1) {
			if (Input.GetButtonDown ("Fire1")) {
				internalArray [currentlySelected].p_selected = true;
				
				switch(currentPlayer)
				{
				case 1:
					God.player1Character = (God.CharacterEnum)currentlySelected;
					break;
				case 2:
					God.player2Character = (God.CharacterEnum)currentlySelected; 
					break;
				case 3:
					God.player3Character = (God.CharacterEnum)currentlySelected;						
					break;
				case 4:
					God.player4Character = (God.CharacterEnum)currentlySelected;
					break;
				}
				currentPlayer++;	
			}
		}
	}
	
	void PressStart(){
	
	}
	void SetSelected(int index){
		for (int i = 0; i < internalArray.Length; i++) 
		{
			if(i == index) 
			{
				if(internalArray[i].p_selected)
				{
					if(movingRight)
						currentlySelected++;
					else
						currentlySelected--;
				}
				else
					internalArray[i].p_hovered = true;
			}
			else 
			{ 
				internalArray[i].p_hovered = false; 
			}
		}
	}
}
