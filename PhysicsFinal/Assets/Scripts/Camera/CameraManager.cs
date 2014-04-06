using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float MAX_BOUND_Y;
	public float MAX_BOUND_X;

	public float MIN_BOUND_Y;
	public float MIN_BOUND_X;

	private Vector3 toFollow;
	private Camera cameraReference;
	private float cameraHeight;
	private float cameraWidth;


	// Use this for initialization
	void Start () {
		cameraReference = Camera.main;
		cameraHeight = cameraReference.orthographicSize * 2.0f;
		cameraWidth = cameraHeight * cameraReference.aspect;
		
		//SetBoundaries(-10,10,10,-10);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(distanceBetween.x);
		this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, MIN_BOUND_X + cameraWidth/2, MAX_BOUND_X - cameraWidth/2),
		                                    						    Mathf.Clamp(this.transform.position.y, MAX_BOUND_Y + cameraHeight/2, MIN_BOUND_Y - cameraHeight/2),
		                                    							transform.position.z);
	}

	public void SetBoundaries(int minX, int maxX, int minY, int maxY)
	{
		MIN_BOUND_X = minX;
		MAX_BOUND_X = maxX;
		MIN_BOUND_Y = minY;
		MAX_BOUND_Y = maxY;// + cameraHeight/2;
	}
}
