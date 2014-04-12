using UnityEngine;
using System.Collections;

public class FireRateModifier : GunModifier {

	// Use this for initialization
	public float FireRateScalar = 0.5f;

	public override void Equip (Gun parent)
	{
		parent.FireRate *= FireRateScalar;
		base.Equip (parent);
	}
	public override void UnEquip ()
	{
		parent.FireRate /= FireRateScalar;
		base.UnEquip ();
	}

}
