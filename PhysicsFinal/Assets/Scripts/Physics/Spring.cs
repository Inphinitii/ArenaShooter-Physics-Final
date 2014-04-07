using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {
	//The two points that the spring connects to
	public Transform springPoint1;
	public Transform springPoint2;

	//spring constant is a measure of the elasticity of the spring 
	//The higher its value, the more the force you will need to exert 
	//to extend the spring. 
	public float konstant;

	//damper to slow the spring down over time
	public float damper;

	//the size of the spring at rest
	public float springSize;
	
	private float mag;
	private Vector3 dist;
	private Vector3 springForce;
	private Vector3 damperForce;
	private Vector3 accel;
	private Vector3 netForce;
	// Use this for initialization
	void Start () 
	{
		Vector3 _temp = springPoint1.transform.position;
		_temp.y += 5;
		springPoint2.transform.position = _temp;
	}
	
	// Update is called once per frame
	void Update () 
	{
		dist = (springPoint2.position - springPoint1.position);

		mag = Mathf.Sqrt((dist.x * dist.x) + (dist.y * dist.y) + (dist.z * dist.z));
		Vector3 normalizedDist = dist / mag;

		springForce = konstant * (dist - (normalizedDist * springSize));
		damperForce = damper * (springPoint2.rigidbody.velocity - springPoint1.rigidbody.velocity);

		netForce += damperForce + springForce;

		accel = netForce / springPoint1.rigidbody.mass;

		springPoint1.rigidbody.velocity += accel * Time.deltaTime;

		//Make sure to reset the net force
		netForce = new Vector3(0,0,0);
	}
}
