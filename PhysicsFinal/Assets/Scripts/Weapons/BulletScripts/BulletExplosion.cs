using UnityEngine;
using System.Collections;

public class BulletExplosion : MonoBehaviour {


	public float m_explosionRadius;
	
	private bool m_exploding;
	private BoxCollider m_ref;
	
	// Use this for initialization
	void Start () {
		m_ref = GetComponent<BoxCollider>();
		m_ref.size = Vector3.zero;
		Explode();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_exploding){
			if(m_ref.size.x < m_explosionRadius){
				m_ref.size = new Vector3(Mathf.Lerp(m_ref.size.x, m_explosionRadius, 2.0f),
				                       Mathf.Lerp(m_ref.size.y, m_explosionRadius, 2.0f),
				                       20.0f);
			}
			else
			{
				m_exploding = false;
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		Vector3 direction = collider.transform.position - transform.position;
		float magnitude = direction.magnitude;
		Debug.Log("Hit");
		collider.rigidbody.AddForce(direction * 100/magnitude, ForceMode.Impulse);
	}
	
	public void Explode(){
		m_exploding = true;
	}
	
}
