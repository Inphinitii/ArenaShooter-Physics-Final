using UnityEngine;
using System.Collections;

public class SpringRemake : MonoBehaviour {
	
	public Transform Anchor;
	public Transform Platform;
	
	public float springConstant;
	public float springDampener;
	public float springAtRest;
	
	private Vector3 distanceBetweenObjects;
	private Vector3 springForce;
	private Vector3 dampeningForce;
	private Vector3 netForce;
	
	Vector3 previousPosition;
	
	// Use this for initialization
	void Start () {
		previousPosition = Platform.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		distanceBetweenObjects = Anchor.position - Platform.position;
		
		//F = -kX - bv
		springForce = springConstant * (distanceBetweenObjects - (springAtRest * distanceBetweenObjects.normalized));
		dampeningForce = springDampener * (Anchor.rigidbody.velocity - Platform.rigidbody.velocity);
		
		netForce = (springForce + dampeningForce);
		Debug.Log(netForce);
		Platform.rigidbody.velocity += (0.5f * (netForce/Platform.rigidbody.mass)) * Time.deltaTime;
		previousPosition = Platform.transform.position;
		
		
	}
}
