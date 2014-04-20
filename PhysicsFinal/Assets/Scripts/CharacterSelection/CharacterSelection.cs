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
	
	private bool start;
	public static bool handleInput = false;
	public static int playerNumber;
	public static int currentPlayer;
	
	private float timer;
	private float m_keyTimer = 0.1f;
	private bool m_takeInput;
	// Use this for initialization
	void Start () {
		currentPlayer = 1; //Player 1 gets first selection
		playerNumber = p_numberOfPlayers;
		internalArray = new Portrait[p_numberOfCharacters];
		start = false;
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
		if(handleInput)
		{
			SetSelected (currentlySelected);
			PlayerSelection();
				
			if(currentPlayer == p_numberOfPlayers + 1){
				pressStart.SetActive(true);
				start = true;	
				handleInput = false;		
			}	
			else
			{
				HandleInput();
			}	
		}
		else
		{
			PressStart();
		}
	}

	void HandleInput() { 

		timer += Time.deltaTime;
		if(timer > m_keyTimer){
			m_takeInput = true;
		}
		
		if (Input.GetAxis("Hori_Dpad_" + currentPlayer) > 0 && m_takeInput || Input.GetKey (KeyCode.RightArrow) && m_takeInput) 
		{
			currentlySelected++;
			movingRight = true;
			m_takeInput = false;
			timer = 0;
			
		} 
		else if (Input.GetAxis("Hori_Dpad_" + currentPlayer) < 0 && m_takeInput || Input.GetKey (KeyCode.LeftArrow) && m_takeInput) 
		{
			currentlySelected--;
			movingRight = false;
			m_takeInput = false;
			timer = 0;
		}
		

		if (currentlySelected >= internalArray.Length) 
		{
			currentlySelected = 0;
		} 
		else if(currentlySelected < 0) 
		{
			currentlySelected = internalArray.Length -1;
		}

	}
	
	void PlayerSelection(){
		if (currentPlayer != internalArray.Length + 1) {
			if (Input.GetAxis("A_"+currentPlayer) > 0 || Input.GetKeyDown(KeyCode.Space)) {
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
		if(start)
		{
			if (Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Space))
			{
				iTween.CameraFadeAdd();
				iTween.CameraFadeTo(iTween.Hash("amount",1.0,"delay",0.1,"time", 2.5, "onComplete","SwitchToRunning","onCompleteTarget",gameObject));
			}
		}
	}
	
	void SwitchToRunning(){
		GameManager.SwitchState(GameManager.GameState.Running);
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
