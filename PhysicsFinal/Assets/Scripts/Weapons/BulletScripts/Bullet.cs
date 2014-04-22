using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int m_bulletDamage = 10;
	
	public GameObject m_bulletExplosion;
	public GameObject m_particleExplosion;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(){
		Instantiate(m_particleExplosion, transform.position, transform.rotation);
		//Instantiate(m_bulletExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	
	public virtual int ApplyDamage(int _health){
		return _health -= m_bulletDamage;
	}
	
}
