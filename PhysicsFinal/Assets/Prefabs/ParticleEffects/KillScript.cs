using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	public float deathTimer;
	private float currentTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if(currentTime >= deathTimer){
			Destroy(gameObject);
		}
	}
}
