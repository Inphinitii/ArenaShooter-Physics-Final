using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public MainMenuManager p_reference;
	public bool m_isPressed;
	public bool m_isHovered;
	
	// Use this for initialization
	void Start () {
		m_isPressed = false;
		m_isHovered = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_isPressed){
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash("x", 0, "easeType", "easeOutQuart", "time", 2.0f, "oncompletetarget", p_reference.gameObject, "oncomplete", "SetBool"));
			m_isPressed = false;
		}
	}
}
