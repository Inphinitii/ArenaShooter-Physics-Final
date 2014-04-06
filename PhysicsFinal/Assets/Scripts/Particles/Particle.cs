using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Particle : MonoBehaviour {
	
	//TODO
	//-- Tween Sizes
	
	public static int ExistingParticles = 0;
	
	private Vector2 m_directionalVector;
	private float m_particleSpeed;
	private bool m_isAlive;
	
	private bool m_isFading;
	private bool m_destroyOnAlpha;
	private float m_fadeTimer;
	private float m_alpha = 1.0f;
	
	// Use this for initialization
	void Start () {
		Alive = true;
		ExistingParticles++;
	}
	
	// Update is called once per frame
	void Update () {
		if(Alive)
		{
			rigidbody2D.velocity = Direction * Speed;
			
			if(IsFading)
				FadeAway();
				
		}
		
		if(m_alpha <= 0)
		{
			Alive = false;	
			
			if(m_destroyOnAlpha){
				Kill();
			}
		}
	}
	
	void FadeAway(){
		Color _temp = renderer.material.color;
		_temp.a = m_alpha -= Time.deltaTime / Fade;
		renderer.material.color = _temp;
	}
	
	void Kill(){
		Destroy (gameObject);
		ExistingParticles--;
	}
	
	public Vector2 Direction{
		get { return m_directionalVector; }
		set { m_directionalVector = value;}
	}
	
	public float Speed{
		get { return m_particleSpeed; }
		set { m_particleSpeed = value;}
	}
	
	public bool Alive{
		get { return m_isAlive; }
		set { m_isAlive = value;}
	}
	
	//Alpha Options
	public float Fade{
		get { return m_fadeTimer; }
		set { m_fadeTimer = value;}
	}
	
	public bool IsFading{
		get { return m_isFading; }
		set { m_isFading = value;}
	}
	
	public bool DestroyOnAlpha{
		get { return m_destroyOnAlpha; }
		set { m_destroyOnAlpha = value;}
	}
}
