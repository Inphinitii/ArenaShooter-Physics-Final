using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LiveActor : MonoBehaviour {
	
	public GameObject m_deathParticles;
	private int m_playerHealth;
	private int m_playerLives;
	private bool m_playerAlive;
	public bool m_isInWater;
	// Use this for initialization
	void Start () {
		m_playerHealth = 10;
		m_playerLives = 5;
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
		gameObject.SetActive(false);
		Instantiate(m_deathParticles, transform.position, Quaternion.identity);
		
		if(m_playerLives > 0)
			Respawn();
		else{
			God.currentPlayers[GetComponent<PlayerController>().PlayerNumber-1] = null;
			Destroy (this.gameObject);
		}
	}
	
	public void Respawn(){
		PlayerAlive = true;
		m_playerHealth = 10;
		m_playerLives--;
		transform.position = God.spawnLocations[Random.Range(0,5)];
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.SetActive(true);
	}
	public bool PlayerAlive{
		get { return m_playerAlive; }
		set { m_playerAlive = value;}
	}
}
