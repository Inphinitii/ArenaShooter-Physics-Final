using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseParticleSystem {
	
	public bool Updating = false;
	protected List<Particle> m_particleList;
	protected Particle[] m_particleReferences; //References for the particle objects from the inspector. Used to instantiate new particles in subclasses.
	protected int m_particleNumber; 
	protected float m_particleSpeed;
	protected Vector3 m_Position;
	
	//Fading Options -- Sent to each individual Particle object
	protected bool m_particleFade;
	protected bool m_destroyOnFade;
	protected float m_particleFadeTime;
	
	//General Initialization
	public virtual void Initialize()
	{
		m_particleList = new List<Particle>();
	}
	
	public virtual void Update(){}
	public virtual IEnumerator UpdateAtTime(float _time){ yield return null;}
	
	//Speed
	public virtual void SetParticleSpeed(Particle _temp){ _temp.Speed = m_particleSpeed;}
	
	//Direction
	public virtual void SetParticleDirection(Particle _temp, Vector2 _direction){ _temp.Direction = _direction;}
	public virtual void SetParticleDirection(Particle _temp, int _index){}
	public virtual void SetParticleFade(Particle _temp){ 
		_temp.Fade = m_particleFadeTime; 
		_temp.DestroyOnAlpha = m_destroyOnFade; 
		_temp.IsFading = m_particleFade;
	}
	
	#region Accessors
	public Particle[] ParticleReference{
		get { return m_particleReferences; }
		set { m_particleReferences = value;}
	}
	
	public int ParticleNumber{
		get { return m_particleNumber; }
		set { m_particleNumber = value;}
	}
	
	public float ParticleSpeed{
		get { return m_particleSpeed; }
		set { m_particleSpeed = value;}
	}
	
	public Vector3 Position{
		get { return m_Position; }
		set { m_Position = value;}
	}
	
	public float FadeTime{
		get { return m_particleFadeTime; }
		set { m_particleFadeTime = value;}
	}
	public bool IsFading{
		get { return m_particleFade; }
		set { m_particleFade = value;}
	}
	
	public bool DestroyOnFade{
		get { return m_destroyOnFade; }
		set { m_destroyOnFade = value;}
	}
	#endregion
}
