using UnityEngine;
using System.Collections;

public class Burst : BaseParticleSystem {
	
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
			SetParticleDirection(_temp, i);
			SetParticleFade(_temp);
			m_particleList.Add (_temp);
		}
	}

	//Override the base classes SetParticleDirection method
	public override void SetParticleDirection(Particle _particle, int _index){
		float _inRadians = (360/m_particleNumber) * (3.14f/180);
		_particle.Direction = new Vector2(Mathf.Sin(_inRadians * _index),
										  Mathf.Cos(_inRadians * _index));
	}
}
