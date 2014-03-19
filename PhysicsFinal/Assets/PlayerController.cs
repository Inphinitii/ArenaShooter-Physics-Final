using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]

public class PlayerController : MonoBehaviour {
	
	Movement movement;
	
	bool Jumping = false;
	
	// Use this for initialization
	void Start () {
		movement = gameObject.GetComponent<Movement>();
	}
	
	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Jumping = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		movement.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (Jumping)
		{
			movement.Jump(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0));
			Jumping = false;
		}
	}
}
