using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
	
	// Use this for initialization
	public float GroundedMoveForce = 200;
	public float AirborneHorizontalForce = 50;
	public float AirborneVerticalForce = 150;
	public float LandDrag = 12;
	public float MaxSpeed = 12;
	public float JumpImpulse = 18;
	public int ExtraJumps = 1;
	public bool Grounded = false;
	public bool MovementLocked = false;
	
	private int ExtraJumpsLeft = 0;
	private float AirDrag;
	
	void Start ()
	{
		AirDrag = rigidbody.drag;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void FixedUpdate()
	{
		CheckGrounded();
	}
	
	public void Move(float horizontalScalar, float verticalScalar)
	{
		if (!MovementLocked && ((horizontalScalar > 0 && rigidbody.velocity.x < MaxSpeed) || (horizontalScalar < 0 && rigidbody.velocity.x > -MaxSpeed)))
		{
			if (Grounded)
			{
				rigidbody.AddForce(new Vector3(GroundedMoveForce * horizontalScalar, 0, 0));
			}
			else
			{
				rigidbody.AddForce(new Vector3(AirborneHorizontalForce * horizontalScalar, 0, 0));
			}
		}
		
		rigidbody.AddForce(new Vector3(0, AirborneVerticalForce * Mathf.Clamp(verticalScalar, -1f, 0), 0));
		
		if (Grounded)
		{
			rigidbody.AddForce(new Vector3(0, AirborneVerticalForce * Mathf.Clamp (verticalScalar, 0f, 1f) / 5, 0));
		}
		
		if (!MovementLocked)
		{
			if (horizontalScalar < 0)
			{
				transform.rotation = Quaternion.Euler(0, 180, 0);
			}
			else if (horizontalScalar > 0)
			{
				transform.rotation = Quaternion.Euler(Vector3.zero);
			}
		}
	}
	
	public void Jump()
	{
		Jump (Vector3.up);
	}
	
	public void Jump(Vector3 direction)
	{
		
		if(Grounded)
		{
			rigidbody.drag = AirDrag;
			rigidbody.AddForce(Vector3.up * JumpImpulse, ForceMode.Impulse);
		}
		else
		{
			Vector3 bufferedForce = new Vector3(direction.x / 2, Mathf.Clamp(direction.y + 0.5f, 0f, 0.5f), 0) * JumpImpulse * 1.25f;
			if (ExtraJumpsLeft >= 1)
			{
				if ((bufferedForce.x > 0 && rigidbody.velocity.x > 0) || bufferedForce.x < 0 && rigidbody.velocity.x < 0)
				{
					bufferedForce.x = 0;
				}
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
				rigidbody.AddForce(bufferedForce, ForceMode.Impulse);
				ExtraJumpsLeft--;
			}
		}
	}
	
	void CheckGrounded()
	{
		if (Physics.Raycast (transform.position, -Vector3.up, collider.bounds.extents.y + 0.3f))
		{
			Grounded = true;
			rigidbody.drag = LandDrag;
			ExtraJumpsLeft = ExtraJumps;
		}
		else
		{
			rigidbody.drag = AirDrag;
			Grounded = false;
		}
	}
	
	//	void OnCollisionStay(Collision collision)
	//	{
	//		if(collision.contacts.Length > 0)
	//		{
	//			ContactPoint contact = collision.contacts[0];
	//			if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
	//			{
	//				Grounded = true;
	//				rigidbody.drag = LandDrag;
	//				ExtraJumpsLeft = ExtraJumps;
	//			}
	//		}
	//	}
	
	//	void OnCollisionExit(Collision collision)
	//	{
	//		if(collision.contacts.Length > 0)
	//		{
	//			ContactPoint contact = collision.contacts[0];
	//			if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
	//			{
	//				Grounded = false;
	//				rigidbody.drag = AirDrag;
	//			}
	//		}
	//	}
}
