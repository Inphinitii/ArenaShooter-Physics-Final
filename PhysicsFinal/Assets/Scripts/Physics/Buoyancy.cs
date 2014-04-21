using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Buoyancy - this script is used to make a box float	*
 * in a body of water using buoyancy physics			*
 * 														*
 * Usage - attach the script to the waterbody(a cube)	*
 * and attach a cube to the box Transform variable		*
 * 														*
 * WARNING - this code will only work for a cube that	*
 * only moves up and down on the Y axis. No rotation 	*
 * can be applied to the cube. Make sure to lock the	*
 * X and Z axis.										*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class Buoyancy : MonoBehaviour {
	//public Transform box; //box that is floating in the water
	public float damper; //a damper that will fake friction
	private Vector3 gravity = new Vector3(0.0f, 9.8f, 0.0f);

	//fluid variables
	private Vector3 buoyantForce; //the force that will act upon the cube when in water
	private Vector3 damperForce; //the force that is added to fake a friction force
	private float waterLine; //the surface of the water
	private float waterLeft; //the left side of the water area
	private float waterRight; //the right side of the water area
	private float fluidDensity; //the density of the liquid in 'kg/L'
	private float appliedDamper; //the damper that is pluged into the damperForce equation


	//box variables
	public float boxDensity; //the density of the box in 'kg/L' (wood is 0.4 kg/L, aluminum is 2.70 kg/L)

	private Vector3 acceleration; //the acceleration of the box in 'm/s^2'
	private Vector3 netForce; //the sum of all forces acting upon the box in 'N'
	private List<Transform> boxes;
	private List<Vector3> boxSize; //the length, width and depth of the box sides
	private List<float> boxVolume; //volume of the box in 'L'
	private float bottom; //the position of the bottom of the box
	private float top; //the position of the top of the box
	private float boxLeft;
	private float boxRight;

	// Use this for initialization
	void Start () 
	{
		/************************
		 * Water initialization	*
		 ************************/
		boxes = new List<Transform>();
		boxSize = new List<Vector3>();
		boxVolume = new List<float>();
		//the waterLine is the top of the body of water or the surface of the water
		waterLine = this.transform.position.y + (0.5f * this.transform.lossyScale.y);

		waterLeft = this.transform.position.x - (0.5f * this.transform.lossyScale.x);

		waterRight = this.transform.position.x + (0.5f * this.transform.lossyScale.x);

		//the density of water is 1.0 kg/L. This is important to know as objects with a density
		//greater than this should sink, while objects with a density less than this should float
		fluidDensity = 1.0f;

		/************************
		 * Box initialization	*
		 ************************/

		//get the size of every side of the box (legth, width and depth)
		//boxSize = boxes.transform.lossyScale;

		/*
		 *	Calculate the volume of the cube.
		 *	Volume of a rectangular prism = Width * Height * Depth.
		 *	The box size is in meters and must be converted to litres (1m^3 = 1000L)
		 */

		//change the mass based on the density of the box and volume
		//box.rigidbody.mass = boxDensity * boxVolume;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the scale of each box contained.
		
		if(boxes.Count > 0){
			for(int i = 0; i < boxes.Count; i++){
				boxSize.Add(boxes[i].transform.lossyScale);
			}
			
			for(int k = 0; k < boxSize.Count; k++){
				boxVolume.Add((boxSize[k].x * boxSize[k].y * boxSize[k].z) * 1000);
			}
			
			//For each box, get their properties.
			
			for(int j = 0; j < boxes.Count; j++){
				bottom = boxes[j].transform.position.y - (0.5f * boxSize[j].y);
				top = boxes[j].transform.position.y + (0.5f * boxSize[j].y);
				boxLeft = boxes[j].transform.position.x - (0.5f * boxSize[j].x);
				boxRight = boxes[j].transform.position.x + (0.5f * boxSize[j].x);
				
				if (bottom > waterLine)
				{
					//when the cube is above the water no buoyant force will act upon the cube
					buoyantForce = new Vector3(0, 0, 0);
					//damperforce to simulate air resistance
					appliedDamper = damper / 1.2f;
					Debug.Log("above Water");
				}
				if (top < waterLine && boxRight > waterLeft && boxLeft < waterRight)
				{
					/*
				 	 *	Calculate the volume of the cube when completely submerged in water.
				 	 *	Volume of a rectangular prism = Width * Height * Depth.
				 	 *	The box size is in meters and must be converted to litres (1m^3 = 1000L)
				 	 */
				 	 
					//boxVolume = (boxSize.x * boxSize.y * boxSize.z) * 1000;
					
					//use the calculated volume to get the buoyant force acting on the cube.
					buoyantForce = fluidDensity * gravity * (boxVolume[j]);
					
					//damperForce to simulate the resistance upon a completely submerged box
					appliedDamper = damper * 2.0f;
		
					Debug.Log("below Water");
				}
				if (top > waterLine && bottom < waterLine && boxRight > waterLeft && boxLeft < waterRight)
				{
					/*	
					 *	calculate the volume of the cube when partially under the water.
					 *	Volume of a rectangular prism = Width * Height * Depth,
					 *	depth and width dont change so long as you move in 2d
					 *	Height must be found by getting the difference in the
					 *	waterline and the bottom of the Cube.
					 *	The box size is in meters and must be converted to litres (1m^3 = 1000L)
					 */
					boxVolume[j] = (boxSize[j].x * (waterLine - bottom) * boxSize[j].z) * 1000;
					
					//use he calculated volume to get the buoyant force acting on the cube.
					buoyantForce = fluidDensity * gravity * (boxVolume[j]);
					
					//damperForce to simulate the resistance upon a partially submerged box
					appliedDamper = damper * (waterLine - bottom);
		
					Debug.Log("partially submerged");
				}
		
				Debug.Log(boxLeft + " " + waterRight);
				/*
				 *	Calculating the Net Force
				 *	^^^^^^^^^^^ ^^^ ^^^ ^^^^^
				 *
				 *	Now that we have the buoyant force we can add in 
				 *	the damping force which will fake friction for us
				 */
				
				//reduce the box velocity by the damper, this is the damperForce
				damperForce = -appliedDamper * boxes[j].rigidbody.mass * (boxes[j].rigidbody.velocity);
				
				//finaly get the net force using the buoyant force and damper force
				netForce += buoyantForce + damperForce;
		
				acceleration = netForce / boxes[j].rigidbody.mass;
				
				boxes[j].rigidbody.velocity += acceleration * Time.deltaTime;
		
				netForce = new Vector3(0,0,0);
			}
		}
		
	}
	
	void OnTriggerEnter(Collider collider){
		boxes.Add(collider.transform);
	}
	
	void OnTriggerExit(Collider collider){
		boxes.Remove(collider.transform);
	}
}
