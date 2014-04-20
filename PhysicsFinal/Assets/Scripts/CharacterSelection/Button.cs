using UnityEngine;
using System.Collections;

public delegate void ButtonFunction();

public class Button : MonoBehaviour {
	
	public MainMenuManager p_reference;
	public bool m_isPressed;
	public bool m_isHovered;
	public AudioClip SelectSound;
	
	public Vector3 originalScale;
    // Use this for initialization
	void Start () {
		m_isPressed = false;
		m_isHovered = false;
		originalScale = transform.localScale;
		gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_isHovered){
			iTween.PunchScale(gameObject, iTween.Hash("x", 0.2f, "easeType", "easeInQuart", "loopType", "loop", "time", 2.0f));
		}
		else
		{
			iTween.Stop(gameObject);
			transform.localScale = originalScale;
		}
	}

	public void ButtonPress(ButtonFunction _delegate){
		audio.PlayOneShot(SelectSound);
		_delegate(); //Call the delegates function
	}
	
	

}
