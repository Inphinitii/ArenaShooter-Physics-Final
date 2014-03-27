using UnityEngine;
using System.Collections;

public class PhysicsAnimationController : MonoBehaviour {

	// Use this for initialization
    public Animator currentAnimator;
    public Movement movement;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        currentAnimator.SetFloat("VelocityY", rigidbody.velocity.y);
        currentAnimator.SetFloat("VelocityX", rigidbody.velocity.x);
        currentAnimator.SetFloat("AbsoluteVelocityY", Mathf.Abs(rigidbody.velocity.y));
        currentAnimator.SetFloat("AbsoluteVelocityX", Mathf.Abs(rigidbody.velocity.x));
        currentAnimator.SetBool("isGrounded", movement.Grounded);
	}
}
