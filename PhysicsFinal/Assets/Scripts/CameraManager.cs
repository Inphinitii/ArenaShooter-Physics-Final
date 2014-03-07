using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Transform itemToFollow;

	private float MAX_BOUND_Y;
	private float MAX_BOUND_X;

	private float MIN_BOUND_Y;
	private float MIN_BOUND_X;

	private Camera cameraReference;
	private float cameraHeight;
	private float cameraWidth;


	// Use this for initialization
	void Start () {
		cameraReference = Camera.main;
		cameraHeight = cameraReference.orthographicSize * 2.0f;
		cameraWidth = cameraHeight * cameraReference.aspect;
		
		SetBoundaries(-10,10,10,-10);
	}
	
	// Update is called once per frame
	void Update () {


		//Clamp the X and Y to the set boundaries

		if (Input.GetKey (KeyCode.D))
		{
			itemToFollow.transform.position = new Vector3(itemToFollow.transform.position.x + 0.5f,
			                                              itemToFollow.transform.position.y,
			                                              itemToFollow.transform.position.z);
		}
		if (Input.GetKey (KeyCode.A))
		{
			itemToFollow.transform.position = new Vector3(itemToFollow.transform.position.x - 0.5f,
			                                              itemToFollow.transform.position.y,
			                                              itemToFollow.transform.position.z);
		}
		if (Input.GetKey (KeyCode.W))
		{
			itemToFollow.transform.position = new Vector3(itemToFollow.transform.position.x,
			                                              itemToFollow.transform.position.y + 0.5f,
			                                              itemToFollow.transform.position.z);
		}
		if (Input.GetKey (KeyCode.S))
		{
			itemToFollow.transform.position = new Vector3(itemToFollow.transform.position.x,
			                                              itemToFollow.transform.position.y - 0.5f, 
			                                              itemToFollow.transform.position.z);
		}

		this.transform.position = new Vector3(itemToFollow.transform.position.x,
		                                      itemToFollow.transform.position.y,
		                                      this.transform.position.z);
		
		this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, MIN_BOUND_X + cameraWidth/2, MAX_BOUND_X - cameraWidth/2),
		                                      Mathf.Clamp(this.transform.position.y, MAX_BOUND_Y + cameraHeight/2, MIN_BOUND_Y - cameraHeight/2),
		                                      this.transform.position.z);
	}

	public void SetBoundaries(int minX, int maxX, int minY, int maxY)
	{
		MIN_BOUND_X = minX;
		MAX_BOUND_X = maxX;
		MIN_BOUND_Y = minY;
		MAX_BOUND_Y = maxY;// + cameraHeight/2;
	}
}
