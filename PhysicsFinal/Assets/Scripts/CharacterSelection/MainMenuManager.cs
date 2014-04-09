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
	
	private static Vector3 m_mainMenuLocation = new Vector3(-30,0,0);
	private static Vector3 m_characterMenuLocation = new Vector3(0,0,0);
	private static Vector3 m_optionMenuLocation = new Vector3(-60,0,0);
	
	
	private Menu m_currentMenu = Menu.MainMenu;
	private Menu m_previousMenu;
	// Use this for initialization
	void Start () {
		p_mainMenuButtons[0].p_reference = this;
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_currentMenu)
		{
			case Menu.MainMenu:
			TransitionTo(Menu.MainMenu);
				MainMenuInput();
				SetSelected(Menu.MainMenu, m_mainMenuSelection);
				break;
			case Menu.CharacterSelect:
			TransitionTo(Menu.CharacterSelect);
				CharacterSelectInput();
				SetSelected(Menu.CharacterSelect, m_characterMenuSelection);
				break;
			case Menu.Options:
			TransitionTo(Menu.Options);
				OptionsInput();
				SetSelected(Menu.Options, m_optionsMenu);			
				break;
		}

		if (Input.GetKeyDown (KeyCode.Backspace)) {
			m_currentMenu = m_previousMenu;
		}
	}
	
	void TransitionTo(Menu _menu){
		switch(_menu)
		{
		case Menu.MainMenu:
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", m_mainMenuLocation.x, 
			                                                  "easeType", "easeOutQuart", 
			                                                  "time", 2.0f,
			                                                  "oncompletetarget", gameObject,
			                                                  "oncomplete" , "SetSelectionInput",
			                                                  "oncompleteparams", false));
			break;
		case Menu.CharacterSelect:
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", m_characterMenuLocation.x, 
			                                                  "easeType", "easeOutQuart", 
			                                                  "time", 2.0f,
			                                                  "oncompletetarget", gameObject,
			                                                  "oncomplete" , "SetSelectionInput",
			                                                  "oncompleteparams", true));
			break;
		case Menu.Options:
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", m_optionMenuLocation.x,
			                                                  "easeType", "easeOutQuart", 
			                                                  "time", 2.0f));  
			break;
		}
	}
	
	void MainMenuInput(){
		
		if(Input.GetButtonDown ("Fire1") || Input.GetKeyDown(KeyCode.Space)){
			ButtonFunction buttonFunction = new ButtonFunction(Foo);
			switch(m_mainMenuSelection){
			case 0:
				buttonFunction = new ButtonFunction(StartButton);
			break;
			case 1:
				buttonFunction = new ButtonFunction(OptionsButton);
				break;
			case 2:
			break;
			}
		
		
			p_mainMenuButtons[m_mainMenuSelection].ButtonPress(buttonFunction);
			p_mainMenuButtons[m_mainMenuSelection].m_isPressed = true;
		}
		
		//Move the current selection up/down
		if (Input.GetKeyDown (KeyCode.W)){m_mainMenuSelection--;} 
		else if (Input.GetKeyDown (KeyCode.S)){m_mainMenuSelection++;}
		
		//Cycle the menu if you go passed a boundary.
		if (m_mainMenuSelection >= p_mainMenuButtons.Length) {m_mainMenuSelection = 0;} 
		else if(m_mainMenuSelection < 0){m_mainMenuSelection = p_mainMenuButtons.Length -1;}
	}
	
	void Foo(){
		Debug.Log("foo");
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
	
	//BUTTON FUNCTIONS
	void StartButton(){
		m_previousMenu = m_currentMenu;
		m_currentMenu = Menu.CharacterSelect;
		CharacterSelection.handleInput = true;
	}
	
	void OptionsButton(){
		m_previousMenu = m_currentMenu;
		m_currentMenu = Menu.Options;	
	}

	void SetSelectionInput(bool _input){
		CharacterSelection.handleInput = _input;
	}
}
