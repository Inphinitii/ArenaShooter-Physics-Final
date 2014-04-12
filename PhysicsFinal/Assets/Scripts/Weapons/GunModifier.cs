using UnityEngine;
using System.Collections;

public class GunModifier : MonoBehaviour {

	// Use this for initialization
    public Gun parent;
	
	public virtual void Equip(Gun parent)
	{
		this.parent = parent;
	}
	public virtual void UnEquip()
	{
		this.parent = null;
	}
}
