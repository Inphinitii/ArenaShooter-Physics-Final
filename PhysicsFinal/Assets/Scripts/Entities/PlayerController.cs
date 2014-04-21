using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]

public class PlayerController : MonoBehaviour {
	
	Movement movement;
    public Gun gun;
    public int PlayerNumber = 1;
	bool Jumping = false;
	
	// Use this for initialization
	void Start () {
		movement = gameObject.GetComponent<Movement>();
	}
	
	void Update()
	{
        if (Input.GetButtonDown("A_" + PlayerNumber))
		{
			Jumping = true;
		}
		
        float RHorizontal = Input.GetAxis("RHorizontal_" + PlayerNumber);
        float RVertical = Input.GetAxis("RVertical_" + PlayerNumber);

        gun.Aim(new Vector3(RHorizontal, RVertical, 0));

        if (Input.GetAxis("Triggers_"+PlayerNumber) < 0)
		{
            gun.Fire();
        }

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        movement.Move(Input.GetAxis("Horizontal_" + PlayerNumber), Input.GetAxis("Vertical_" + PlayerNumber));
		if (Jumping)
		{
            movement.Jump(new Vector3(Input.GetAxis("Horizontal_" + PlayerNumber), Input.GetAxis("Vertical_" + PlayerNumber), 0));
			Jumping = false;
		}
	}
}
