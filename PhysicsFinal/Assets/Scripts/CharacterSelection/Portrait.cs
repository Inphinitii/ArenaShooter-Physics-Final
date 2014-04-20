using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Portrait : MonoBehaviour {

	public Sprite p_portraitNormal;
	public Sprite p_portraitHover;
	public Sprite p_portraitLocked;
	public bool p_hovered;
	public bool p_selected;

	private Vector3 m_originalPosition;
	private bool m_disableHover;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = p_portraitNormal;
		p_selected = false;
		m_originalPosition = transform.position;
		m_disableHover = false;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentlySelected ();
	}

	public void CurrentlySelected(){
		if(!m_disableHover){
		
			if (p_hovered && !p_selected) {
					GetComponent<SpriteRenderer> ().sprite = p_portraitHover;
					iTween.MoveBy (gameObject, iTween.Hash ("y", 0.2f, "easeType", "easeInExpo", "loopType", "pingPong"));
			} else {
					GetComponent<SpriteRenderer> ().sprite = p_portraitNormal;
					iTween.Stop(gameObject);
			    	transform.position = m_originalPosition;
			}
		
			if (p_selected) {
				GetComponent<SpriteRenderer> ().sprite = p_portraitLocked;
				transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			}	
		
		}

	}
	
	public void DisableHover(){
		m_disableHover = true;
		
		GetComponent<SpriteRenderer> ().sprite = p_portraitNormal;
		iTween.Stop(gameObject);
		transform.position = m_originalPosition;
	}
}
