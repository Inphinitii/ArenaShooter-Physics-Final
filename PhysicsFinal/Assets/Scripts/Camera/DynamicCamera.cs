using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour {

	public GameObject[] ObjectsToTrack; // This can be converted to a list with no issues if need be.
	Rect WorldBounds = new Rect();
	Vector3 MinVector = Vector3.zero;
	Vector3 MaxVector = Vector3.zero;
    //Camera Control Variables
    public float CameraSmoothing = 8;
    public float StartingDepth;
	float DestinationDepth = -10;
    bool WatchInterest = false;
    Quaternion previousRotation;
    GameObject ObjOfInterest;
    Quaternion DesiredRotation;
    
	public float MAX_BOUND_Y;
	public float MAX_BOUND_X;
	
	public float MIN_BOUND_Y;
	public float MIN_BOUND_X;
    
	private Camera cameraReference;
	private float cameraHeight;
	private float cameraWidth;
	

	// Use this for initialization
	void Start () {
		cameraReference = Camera.main;
		
		RecalculateBounds();
		print(WorldBounds.ToString());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.K) && !WatchInterest){
			ObjectOfInterest(ObjectsToTrack[0],5f);
		}

        if(WatchInterest)
        {
            DesiredRotation = Quaternion.LookRotation(ObjOfInterest.transform.position - transform.position);
        }

		RecalculateBounds();
		CalculateDepthChange();

		cameraReference.orthographicSize = Mathf.Lerp(cameraReference.orthographicSize, DestinationDepth, Time.deltaTime * CameraSmoothing);
		
		transform.position = Vector3.Lerp(transform.position,new Vector3(MinVector.x + (MaxVector.x - MinVector.x)/2,(MinVector.y + (MaxVector.y - MinVector.y)/2) - 1.5f, transform.position.z),Time.deltaTime * CameraSmoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation,DesiredRotation,Time.deltaTime * CameraSmoothing);
        
        //Clamp to level
		cameraHeight = cameraReference.orthographicSize;
		cameraWidth = cameraHeight * cameraReference.aspect * 2.0f;
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_BOUND_X + cameraWidth/2, MAX_BOUND_X - cameraWidth/2),
		                                 Mathf.Clamp(transform.position.y, MAX_BOUND_Y + cameraHeight/2, MIN_BOUND_Y - cameraHeight/2),
		                                 transform.position.z);
	}
	
	void CalculateDepthChange(){
		DestinationDepth = StartingDepth + (Vector3.Magnitude(MaxVector - MinVector) / 4) - 2;
		if(DestinationDepth < 8){
			DestinationDepth = 8;
		}
		else if(DestinationDepth > 13.5){
			DestinationDepth = 13.5f;
		}
	}
	
	void RecalculateBounds()
	{
		float minX = 0;
		float minY = 0;
		float maxX = 0;
		float maxY = 0;
		
		minX = ObjectsToTrack[0].collider.bounds.min.x;
		minY = ObjectsToTrack[0].collider.bounds.min.y;
		maxX = ObjectsToTrack[0].collider.bounds.max.x;
		maxY = ObjectsToTrack[0].collider.bounds.max.y;

		for(int i = 0;i<ObjectsToTrack.Length;i++)
		{
		
			if(ObjectsToTrack[i].collider.bounds.min.x < minX){
				minX = ObjectsToTrack[i].collider.bounds.min.x;
			}
			if(ObjectsToTrack[i].collider.bounds.max.x > maxX){
				maxX = ObjectsToTrack[i].collider.bounds.max.x;
			}
			if(ObjectsToTrack[i].collider.bounds.min.y < minY){
				minY = ObjectsToTrack[i].collider.bounds.min.y;
			}
			if(ObjectsToTrack[i].collider.bounds.max.y > maxY){
				maxY = ObjectsToTrack[i].collider.bounds.max.y;
			}
		}

		WorldBounds.x = minX;
		WorldBounds.y = minY;
		WorldBounds.width = maxX;
		WorldBounds.height = maxY;
		MinVector.x = minX;
		MinVector.y = minY;
		MaxVector.x = maxX;
		MaxVector.y = maxY;
	}
	
	public void ObjectOfInterest(GameObject obj, float timeToWatch)
	{
		ObjOfInterest = obj;
		WatchInterest = true;
		previousRotation = transform.rotation;
		Invoke("Resume",timeToWatch);
	
	}
	void Resume(){
	
		WatchInterest = false;
		DesiredRotation = previousRotation;
	
	}
	
	
	
}
