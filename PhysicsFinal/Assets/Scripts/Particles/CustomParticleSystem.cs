using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomParticleSystem : MonoBehaviour {
	[ExecuteInEditMode]
	
	public enum EmitterType{
		Radial,
		Emitter,
		Trail
	}
	
	public EmitterType p_EmitterType;

	//General Fields
	public int p_NumberOfParticles;
	public float p_ParticleSpeed;
	
	//Repeating Options -- Contained within this class -- Controls the Coroutine(UpdateAtTime())
	public bool p_isRepeating;
	public float p_repeatSpeed;
	
	//Fading Options
	public bool p_isFading;
	public bool p_destroyOnFade;
	public float p_fadeSpeed;

	//Burst Options
	public bool p_rotateTowardsVelocity;
	public bool p_arcBurst;
	public float p_burstArc;
	public float p_rotationBurstInDegrees;

	//Emitter Options
	public float p_arcWidth;
	public float p_rotationInDegrees;

	public Particle[] p_ParticleReferences; //These are the particles sent through the inspector.
	
	//Base Particle System - To be polymorphised
	private BaseParticleSystem m_ParticleSystem;
		
	void Start () {	
		m_ParticleSystem = p_EmitterType == EmitterType.Radial ? m_ParticleSystem = new Burst() :
						   p_EmitterType == EmitterType.Emitter ? m_ParticleSystem = new Emitter() :
						   p_EmitterType == EmitterType.Trail ? m_ParticleSystem = new Trail() :
						   null;
		
		m_ParticleSystem.Position = transform.position;					   				   
		SetProperties();		   
		m_ParticleSystem.Initialize();

		
	}
	
	// Update is called once per frame
	void Update () {
        if(m_ParticleSystem != null)
	    	m_ParticleSystem.Position = transform.position;
            
		if(p_isRepeating)
		{
			if(!m_ParticleSystem.Updating)
			{
				StartCoroutine(m_ParticleSystem.UpdateAtTime(p_repeatSpeed));
			}
		}
		else
		{
			m_ParticleSystem.Update();
		}
	}	
	
	//Pass along the variables from this script to the BaseParticleSystem object. Ugly and unruly code.
	void SetProperties() {
		//Set the protected general values within the base particle system
		m_ParticleSystem.ParticleReference = p_ParticleReferences;
		m_ParticleSystem.ParticleNumber = p_NumberOfParticles;
		m_ParticleSystem.ParticleSpeed = p_ParticleSpeed;	
		
		//Fade
		m_ParticleSystem.IsFading = p_isFading;
		m_ParticleSystem.FadeTime = p_fadeSpeed;
		m_ParticleSystem.DestroyOnFade = p_destroyOnFade;
		
		if(p_EmitterType == EmitterType.Emitter){
			((Emitter)m_ParticleSystem).Arc = p_arcWidth;
			((Emitter)m_ParticleSystem).RotationInDegrees = p_rotationInDegrees;
		}

		if (p_EmitterType == EmitterType.Radial) {
			((Burst)m_ParticleSystem).m_rotateTowardsVelocity = p_rotateTowardsVelocity;
			((Burst)m_ParticleSystem).m_arcBurst = p_arcBurst;
			((Burst)m_ParticleSystem).m_burstArc = p_burstArc;
			((Burst)m_ParticleSystem).m_rotationInDegrees = p_rotationBurstInDegrees;
			//	public bool m_arcBurst;
			//public float m_burstArc;
			//public float m_rotationInDegrees;
		}

	}
}
