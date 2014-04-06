using UnityEngine;
using System.Collections;

public class Emitter : BaseParticleSystem {
	Particle _temp;
	
	private float m_emitterArc;
	private float m_rotationInDegrees;
	private Vector2 m_direction;
	//Default constructor
	public Emitter(){
		m_emitterArc = 0;
		m_rotationInDegrees = 90;
	}
	
	//Per-frame update
	public override void Update (){
		Debug.DrawRay(Position, Vector2.up, Color.red);
	}
	
	//Delayed update
	public override IEnumerator UpdateAtTime(float _time){
		Updating = true;
		yield return new WaitForSeconds(_time);
		
		if(Particle.ExistingParticles != m_particleNumber)
		{
			AddEmitterParticle();
		}
		
		Updating = false;
	}
	
	//Initialization
	public override void Initialize(){
		base.Initialize();
	}
	
	void AddEmitterParticle()
	{
		_temp = MonoBehaviour.Instantiate(m_particleReferences[0], Position, Quaternion.identity) as Particle;
		base.SetParticleSpeed(_temp);
		
		//Directional Vector
		m_direction = new Vector2(Random.Range(Mathf.Sin(0-m_emitterArc/2 * Mathf.Deg2Rad), 
											   Mathf.Sin(0+m_emitterArc/2 * Mathf.Deg2Rad)),
                                  Random.Range(Mathf.Cos(0-m_emitterArc/2 * Mathf.Deg2Rad), 
                                 		       Mathf.Sin(0+m_emitterArc/2 * Mathf.Deg2Rad)));
		
		//Rotate Direction
		Vector2 oldDirection = m_direction;
		m_direction.x = oldDirection.x *  Mathf.Cos (-m_rotationInDegrees * Mathf.Deg2Rad) - oldDirection.y * Mathf.Sin (-m_rotationInDegrees * Mathf.Deg2Rad);
		m_direction.y = oldDirection.x *  Mathf.Sin (-m_rotationInDegrees * Mathf.Deg2Rad) + oldDirection.y * Mathf.Cos (-m_rotationInDegrees * Mathf.Deg2Rad);
		//_direction.Normalize();
		
		base.SetParticleDirection (_temp, m_direction);
		SetParticleFade(_temp);
		m_particleList.Add(_temp);
	}
	//Override the base classes SetParticleDirection method
	public override void SetParticleDirection(Particle _temp, Vector2 _direction){
	}
	
	//Override the base classes SetParticleSpeed method
	public override void SetParticleSpeed(Particle _temp){
		//Set this to give particles random speeds
	}
	
	public float Arc{
		get{ return m_emitterArc; }
		set{ m_emitterArc = value;}
	}
	
	public float RotationInDegrees{
		get{ return m_rotationInDegrees; }
		set{ m_rotationInDegrees = value;}
	}
}
