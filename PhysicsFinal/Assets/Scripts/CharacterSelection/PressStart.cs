using UnityEngine;
using System.Collections;

public class PressStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		iTween.PunchScale(gameObject, iTween.Hash("x", 0.2f, "easeType", "easeInQuad", "loopType", "loop", "time", 2.0f));
	}
}
