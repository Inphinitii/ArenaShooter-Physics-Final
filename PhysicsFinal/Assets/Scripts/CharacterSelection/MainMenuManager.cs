using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	
	public enum Menu{
		MainMenu,
		CharacterSelect,
		Options
	}
	
	public Button[] p_mainMenuButtons;
	public Button[] p_optionMenuButtons;
	
	private int m_mainMenuSelection = 0;
	private int m_characterMenuSelection = 0;
	private int m_optionsMenu = 0;
	
	private static Vector3 m_mainMenuLocation = new Vector3(-25,0,0);
	private static Vector3 m_characterMenuLocation = new Vector3(0,0,0);
	private static Vector3 m_optionMenuLocation = new Vector3(-25,0,0);
	
	
	private Menu m_currentMenu = Menu.MainMenu;
	// Use this for initialization
	void Start () {
		p_mainMenuButtons[0].p_reference = this;
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_currentMenu)
		{
			case Menu.MainMenu:
				MainMenuInput();
				SetSelected(Menu.MainMenu, m_mainMenuSelection);
				break;
			case Menu.CharacterSelect:
				CharacterSelectInput();
				SetSelected(Menu.CharacterSelect, m_characterMenuSelection);
				break;
			case Menu.Options:
				OptionsInput();
				SetSelected(Menu.Options, m_optionsMenu);			
				break;
		}
	}
	
	void TransitionTo(){
		//Translate the camera position using iTween, ease in. x from -25 to 0
		Debug.Log("wat");
	}
	
	void MainMenuInput(){
		
		if(Input.GetButtonDown ("Fire1")){
			p_mainMenuButtons[m_mainMenuSelection].m_isPressed = true;
		}
		
		//Move the current selection up/down
		if (Input.GetAxisRaw ("Vertical") > 0){m_mainMenuSelection++;} 
		else if (Input.GetAxisRaw ("Vertical") < 0){m_mainMenuSelection--;}
		
		//Cycle the menu if you go passed a boundary.
		if (m_mainMenuSelection >= p_mainMenuButtons.Length) {m_mainMenuSelection = 0;} 
		else if(m_mainMenuSelection < 0){m_mainMenuSelection = p_mainMenuButtons.Length -1;}
	}
	
	void CharacterSelectInput(){
		
	}
	
	void OptionsInput(){
	
	}
	
	void SetSelected(Menu _menu, int _index){
		switch(_menu)
		{
		case Menu.MainMenu:
			for(int i = 0; i < p_mainMenuButtons.Length; i++)
			{
				if(i == _index)
					p_mainMenuButtons[i].m_isHovered = true;
				else
					p_mainMenuButtons[i].m_isHovered = false;
			}
			break;
		case Menu.CharacterSelect:
			break;
		case Menu.Options:
			break;
		}
	}
	
	void SetBool(){
		CharacterSelection.handleInput = true;
	}
}
