using UnityEngine;
using System.Collections;

public class Trail : BaseParticleSystem {
	Particle _temp;
	
	//Default constructor
	public Trail(){
	}
	
	//Per-frame update
	public override void Update (){

	}
	
	//Delayed update
	public override IEnumerator UpdateAtTime(float _time){
		Updating = true;
		yield return new WaitForSeconds(_time);
		
		if(Particle.ExistingParticles != m_particleNumber)
		{
			AddTrailParticle();
		}
		
		Updating = false;
	}
	
	//Initialization
	public override void Initialize(){
		base.Initialize();
	}
	
	void AddTrailParticle()
	{
		_temp = MonoBehaviour.Instantiate(m_particleReferences[0], Position, Quaternion.identity) as Particle;
		SetParticleFade(_temp);
		SetParticleSpeed(_temp);
		//SetParticleDirection (_temp, Vector2.up);
		m_particleList.Add(_temp);
	}
}
