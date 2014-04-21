using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour{
	
	public enum GameState{
		MainMenu,
		Running
	}
	
	public GameObject[] playerPrefabs;
	public GUIText m_guiText;
	static GameState m_currentGameState;
	
	//Animation Variables
	private float m_quad1;
	private float m_quad2;
	
	private Texture2D myTex;
	
	private bool onStart;
	private bool m_Introduction;
	private GUIText m_PlayerText;
	
	private GameObject[] m_playerRefs;
	
	
	void Start () {
		DontDestroyOnLoad(this); //Carry over between scenes
		m_currentGameState = GameState.MainMenu; //Default 
		
		m_quad1 = 0;
		m_quad2 = Screen.height/2;

		myTex = FlatTexture(Color.black);
		onStart = true;
		m_Introduction = false;
		
		m_playerRefs = new GameObject[2];
	}
	
	void Update () {
		if(m_Introduction && Input.GetKeyDown (KeyCode.JoystickButton7)){
			EndAnimation();
		}
	}
	
	void OnGUI() { 
		if (m_currentGameState == GameState.MainMenu) 
		{
	
		}
		if(m_currentGameState == GameState.Running)
		{
			if(onStart){
				StartAnimation();
				SpawnCharacters();
				m_PlayerText = Instantiate(m_guiText, new Vector3(-0.5f,0.5f,0), Quaternion.identity) as GUIText;
			}
			
					
			if(m_Introduction){
				GUI.DrawTexture(new Rect(0, m_quad1, Screen.width, Screen.height/2), myTex, ScaleMode.StretchToFill, false,0);
				GUI.DrawTexture(new Rect(0, m_quad2, Screen.width, Screen.height/2), myTex, ScaleMode.StretchToFill, false,0);
			}
			
			
		}
	}
	
	public static void SwitchState(GameState _gameState){
		m_currentGameState = _gameState;
		
		if(m_currentGameState == GameState.MainMenu){
			Application.LoadLevel(0);
		}
		
		else if(m_currentGameState == GameState.Running){
			Application.LoadLevel(1);
		}
	}
	
	//THIS STUFF PROBABLY SHOULDNT BE HERE, BUT ITS GOING HERE BECAUSE TIME CONSTRAINTS AND DONT WANT TO REFACTOR.
	void StartAnimation(){
		Time.timeScale = 0;
		
		DynamicCamera myRef = Camera.main.GetComponent<DynamicCamera>();
		myRef.enabled = false;
		
		Camera.main.transform.position = new Vector3(-3,11.5f,-20);
		Camera.main.orthographicSize = 16.5f;
		
		
		//Original size = 8;
		float m_quad1Bound = m_quad1 - Screen.height / 3;
		float m_quad2Bound = m_quad2 + Screen.height/3;
		
		iTween.ValueTo(gameObject, iTween.Hash("from", m_quad1, 
												"to", m_quad1Bound, 
												"time", 1.5f,
												"easetype", iTween.EaseType.easeOutQuart, 
												"onupdate", "Tween1", 
												"onupdatetarget", gameObject, 
												"ignoretimescale", true));
												
		iTween.ValueTo(gameObject, iTween.Hash("from", m_quad2, 
												"to", m_quad2Bound, 
												"time", 1.5f,
												"easetype", iTween.EaseType.easeOutQuad, 
												"onupdate", "Tween2", 
												"onupdatetarget", gameObject, 
												"ignoretimescale", true, 
												"oncompletetarget", gameObject, 
		                                        "oncomplete", "StartZooms",
		                                        "oncompleteparams", 1));

		onStart = false;
		m_Introduction = true;
	}
	
	void StartZooms(int player){
	
		if(player == 1){
		iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", God.m_spawnLocation1.x, 
		                                                  "easeType", "easeOutQuart", 
		                                                  "time", 2.0f,
		                                                  "ignoretimescale",true));
		                                                  
		iTween.ValueTo(Camera.main.gameObject, iTween.Hash("from", Camera.main.orthographicSize, 
		                                                   "to", 8, 
		                                                   "time", 1.5f,
		                                                   "easetype", iTween.EaseType.easeOutQuad, 
		                                                   "onupdate", "Zoom", 
		                                                   "onupdatetarget", gameObject, 
		                                                   "ignoretimescale", true,
		                                                   "oncompletetarget", gameObject, 
		                                                   "oncomplete", "StartPan",
		                                                   "oncompleteparams", 1));  		

	   }      
	   
	   if(player == 2){
		iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", God.m_spawnLocation2.x, 
		                                                  "easeType", "easeOutQuart", 
		                                                  "time", 2.0f,
		                                                  "ignoretimescale",true));
		
		iTween.ValueTo(Camera.main.gameObject, iTween.Hash("from", Camera.main.orthographicSize, 
		                                                   "to", 8, 
		                                                   "time", 1.5f,
		                                                   "easetype", iTween.EaseType.easeOutQuad, 
		                                                   "onupdate", "Zoom", 
		                                                   "onupdatetarget", gameObject, 
		                                                   "ignoretimescale", true,
		                                                   "oncompletetarget", gameObject, 
		                                                   "oncomplete", "StartPan",
		                                                   "oncompleteparams", 2));  
		}   
		
		if(player == 0){
			iTween.ValueTo(Camera.main.gameObject, iTween.Hash("from", Camera.main.orthographicSize, 
			                                                   "to", 10, 
			                                                   "time", 1.5f,
			                                                   "easetype", iTween.EaseType.easeOutQuad, 
			                                                   "onupdate", "Zoom", 
			                                                   "onupdatetarget", gameObject, 
			                                                   "ignoretimescale", true));  
		}                            
	}
	
	void StartPan(int player){
		if(player == 1){
			iTween.MoveTo(m_PlayerText.gameObject, iTween.Hash("x", 1.5,
															   "easeType", iTween.EaseType.easeOutSine,
															   "time", 9.0f,
															   "ignoretimescale", true));
			
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", God.m_spawnLocation1.x + 10, 
			                                                  "easeType", iTween.EaseType.easeInOutSine, 
			                                                  "time", 7.5f,
			                                                  "ignoretimescale",true,
			                                                  "oncompletetarget", gameObject, 
			                                                  "oncomplete", "StartZooms",
			                                                  "oncompleteparams", 2));
		}
		
		if(player == 2){
		m_PlayerText.text = "Player 2";
		iTween.MoveTo(m_PlayerText.gameObject, iTween.Hash("x", -0.5f,
		                                                   "easeType", iTween.EaseType.easeOutSine,
		                                                   "time", 9.0f,
		                                                   "ignoretimescale", true));
		                                                   
		iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", God.m_spawnLocation2.x - 10, 
			                                              "easeType", iTween.EaseType.easeInOutSine, 
		                                                  "time", 7.5f,
		                                                  "ignoretimescale",true,
		                                                  "oncomplete", "EndAnimation",
		                                                  "oncompletetarget", gameObject));
		}
	}
	
	void EndAnimation(){
		ScreenFlash ();
		Camera.main.GetComponent<DynamicCamera>().enabled = true;
		Camera.main.transform.position = new Vector3(-3,11.5f,-20);
		Camera.main.orthographicSize = 16.5f;
		Time.timeScale = 1;
		m_Introduction = false;
	}
	
	void ScreenFlash(){
		iTween.CameraFadeAdd(FlatTexture (Color.white));
		iTween.CameraFadeTo(iTween.Hash("amount",1.0,
			                            "time", 0.1f));
			                            
		iTween.CameraFadeTo(iTween.Hash("amount",0.0,
		                                "time", 0.1f,
		                                "delay", 0.1f,
		                                "oncomplete", "iTweenStop",
		                                "oncompletetarget", gameObject));
			   
	}
	
	void iTweenStop(){
		iTween.Stop();
	}
	
	void SpawnCharacters(){
		m_playerRefs[0] = God.Spawn(God.player1Character) as GameObject;
		m_playerRefs[1] = God.Spawn(God.player2Character) as GameObject;
		Camera.main.GetComponent<DynamicCamera>().ObjectsToTrack = m_playerRefs;
        
        m_playerRefs[0].GetComponent<PlayerController>().PlayerNumber = 1;
        m_playerRefs[1].GetComponent<PlayerController>().PlayerNumber = 2;
		
	}
	
	void Tween1(float newValue){
		m_quad1 = newValue;
		Debug.Log(m_quad2);
	}
	
	void Tween2(float newValue){
		m_quad2 = newValue;
		Debug.Log(m_quad2);
	}
	
	void Zoom(float newValue){
		Camera.main.orthographicSize = newValue;
	}
	
	Texture2D FlatTexture(Color color) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		return texture;
	}
}
