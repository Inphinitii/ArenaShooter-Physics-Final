using UnityEngine;
using System.Collections;

public class Bouncer : Bullet {

	// Use this for initialization
    public int BouncesBeforeDestroy = 3;
    int current_Bounces = 0;
    public GameObject bounceParticles;
    public GameObject destroyParticles;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter()
    {
        if (current_Bounces < BouncesBeforeDestroy)
        {
            current_Bounces++;
            Instantiate(bounceParticles,transform.position,transform.rotation);

        }
        else
        {
            Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
