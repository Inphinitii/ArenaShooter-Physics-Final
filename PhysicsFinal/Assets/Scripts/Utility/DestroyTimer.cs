using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	// Use this for initialization
    public float time = 1;
	void Start () {
        Invoke("DestroySelf",time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
