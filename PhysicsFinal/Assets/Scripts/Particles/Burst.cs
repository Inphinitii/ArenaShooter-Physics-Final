using UnityEngine;
using System.Collections;

public class Burst : BaseParticleSystem {

	public bool m_rotateTowardsVelocity;
	public bool m_arcBurst;
	public float m_burstArc;
	public float m_rotationInDegrees;
	//Default constructor
	public Burst(){
	}
	
	//Per-frame update
	public override void Update(){
	
	}
	
	//Delayed update
	public override IEnumerator UpdateAtTime(float _time){
		Updating = true;
		yield return new WaitForSeconds(_time);
		Initialize();
		Updating = false;
	}
	
	//Initialization
	public override void Initialize(){
		base.Initialize();
		
		Particle _temp;
		for(int i = 0; i < m_particleNumber; i++)
		{
			_temp = MonoBehaviour.Instantiate(m_particleReferences[0], m_Position, Quaternion.identity) as Particle;
			SetParticleSpeed(_temp);

			if(m_arcBurst)
				SetParticleDirection(_temp, Vector2.up);
			else
				SetParticleDirection(_temp, i);

			SetParticleFade(_temp);
			m_particleList.Add (_temp);
		}
	}

	//Override the base classes SetParticleDirection method
	public override void SetParticleDirection(Particle _particle, int _index){
		float _inRadians = (360/m_particleNumber) * (3.14f/180);
		Vector2 direction = new Vector2(Mathf.Sin(_inRadians * _index),
									    Mathf.Cos(_inRadians * _index));

		_particle.Direction = direction;

		if(m_rotateTowardsVelocity)
			_particle.transform.LookAt((Vector3)direction - Position);
	}

	public override void SetParticleDirection(Particle _particle, Vector2 _direction){
		Vector2 direction = new Vector2(Random.Range(Mathf.Sin(0-m_burstArc/2 * Mathf.Deg2Rad), 
		                                             Mathf.Sin(0+m_burstArc/2 * Mathf.Deg2Rad)),
		                                Random.Range(Mathf.Cos(0-m_burstArc/2 * Mathf.Deg2Rad), 
		            							     Mathf.Sin(0+m_burstArc/2 * Mathf.Deg2Rad)));

		Vector2 oldDirection = direction;
		direction.x = oldDirection.x *  Mathf.Cos (-m_rotationInDegrees * Mathf.Deg2Rad) - oldDirection.y * Mathf.Sin (-m_rotationInDegrees * Mathf.Deg2Rad);
		direction.y = oldDirection.x *  Mathf.Sin (-m_rotationInDegrees * Mathf.Deg2Rad) + oldDirection.y * Mathf.Cos (-m_rotationInDegrees * Mathf.Deg2Rad);

		base.SetParticleDirection (_particle, direction);

		if (m_rotateTowardsVelocity) {
			float angle = Mathf.Atan2(oldDirection.y, oldDirection.x);
			_particle.transform.Rotate (new Vector3(Mathf.Sin (angle), Mathf.Cos (angle), 0));
		}

	}

}
