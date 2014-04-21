using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour {

	// Use this for initialization
    public GameObject player;
    public Transform spawnOrigin;
    public Transform rotatePoint;
    public GunModifier[] modifiers = new GunModifier[3];
    public int current_modifiers = 0;
    public GameObject projectile;
    public float FireForce = 200;
    public float FireRate = 0.3f;
    public float RecoilDampening = 0.7f;
	public float AimSpeed = 3f;
    public bool Firing = false;

	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	}
    public void Fire()
	{
        if (!Firing)
		{
            GameObject bullet = (GameObject)Instantiate(projectile, spawnOrigin.position, spawnOrigin.rotation);
            Physics.IgnoreCollision(player.collider, bullet.collider);

            if (Vector2.Dot((Vector2)(player.rigidbody.velocity).normalized, (Vector2)(player.transform.right)) > 0)
            {
                bullet.rigidbody.velocity += new Vector3(player.rigidbody.velocity.x, 0, 0);
            }
            
            bullet.rigidbody.AddForce(spawnOrigin.right * FireForce, ForceMode.Impulse);
            player.rigidbody.AddForce(-spawnOrigin.right * FireForce * bullet.rigidbody.mass * RecoilDampening,ForceMode.Impulse);
            Firing = true;
            Invoke("CoolDown", FireRate);
        }
    }
    void CoolDown()
    {
        Firing = false;
    }

    public void Aim(Vector3 direction) 
    {
        Vector3 dir = direction.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x);
        
        Quaternion finalRotation = Quaternion.AngleAxis((-angle / Mathf.PI) * 180, Vector3.forward);
        
        if (player.transform.right != Vector3.right)
        {
            if (angle == 0)
            {
                angle = Mathf.PI;
            }
            
            finalRotation = Quaternion.AngleAxis(((-angle - Mathf.PI) / Mathf.PI) * 180, Vector3.forward);
            finalRotation *= Quaternion.AngleAxis(180,Vector3.right);
            finalRotation *= Quaternion.AngleAxis(180, Vector3.forward);
        }
		rotatePoint.rotation = Quaternion.Slerp(rotatePoint.rotation,finalRotation,Time.deltaTime * AimSpeed);
    }

    public bool AddGunModifier(GunModifier m)
    {
        if (current_modifiers < modifiers.Length)
        {
            modifiers[current_modifiers] = m;
            Component mod = gameObject.AddComponent(m.GetType().ToString());
            //EditorUtility.CopySerialized(m, mod);
            ((GunModifier)(mod)).Equip(this);
            current_modifiers++;
            return true;
        }
        return false;
    }
}
