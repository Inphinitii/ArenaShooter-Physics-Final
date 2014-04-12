using UnityEngine;
using System.Collections;

public class SpawnForce : GunModifier {

	// Use this for initialization
	public float SpawnForceAddition = 10;
	public override void Equip (Gun parent)
	{
		base.Equip (parent);
		parent.FireForce += SpawnForceAddition;
	}
	public override void UnEquip ()
	{
		parent.FireForce -= SpawnForceAddition;
		base.UnEquip ();
	}
}
