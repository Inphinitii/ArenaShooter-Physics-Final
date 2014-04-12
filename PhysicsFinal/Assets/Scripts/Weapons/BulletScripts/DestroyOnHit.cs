using UnityEngine;
using System.Collections;

public class DestroyOnHit : Bullet {

	// Use this for initialization
    public GameObject destroyParticles;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter()
    {
        Instantiate(destroyParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
