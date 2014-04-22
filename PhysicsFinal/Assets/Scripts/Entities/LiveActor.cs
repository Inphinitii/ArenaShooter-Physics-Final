using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LiveActor : MonoBehaviour {
	
	public GameObject m_deathParticles;
	private int m_playerHealth;
	private bool m_playerAlive;
	// Use this for initialization
	void Start () {
		m_playerHealth = 100;
		m_playerAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		CheckAlive();
		
		if(!m_playerAlive)
			CleanUp();
	}
	
	void OnCollisionEnter(Collision collider){
		if(collider.gameObject.tag == "Projectile")
			if(collider.gameObject.GetComponent<Bullet>() != null){
					m_playerHealth = collider.gameObject.GetComponent<Bullet>().ApplyDamage(m_playerHealth);
					Debug.Log(m_playerHealth);
				}
	}
	
	void CheckAlive(){
		if(m_playerHealth <= 0){
			m_playerAlive = false;
		}
	}
	
	void CleanUp(){
		Destroy (gameObject);
		Instantiate(m_deathParticles, transform.position, Quaternion.identity);
	}
	
	public void Respawn(){
		PlayerAlive = true;
	}
	public bool PlayerAlive{
		get { return m_playerAlive; }
		set { m_playerAlive = value;}
	}
}
