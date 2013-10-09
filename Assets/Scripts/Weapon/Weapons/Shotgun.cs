using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {
	public Shotgun() {
		name = "Shotgun";
		attackType = AttackType.Range;
		damage = 2;
		ammo = 20;
		maxAmmo = 50;
		coolDown = 3f;
		
		int pelletCount = 10;
		float spreadFactor = 15f;
		
		Vector3 rot = Vector3.zero;
		for (int i = 0; i < pelletCount; i++) {
			rot.x = Random.Range (-spreadFactor,spreadFactor);
			rot.y = Random.Range (-spreadFactor,spreadFactor);
			Debug.Log (rot);
			AddProjectile ("Pellet", rot);
		}
		/*
		AddProjectile ("Bullet");
		AddProjectile ("Bullet", new Vector3(15,15,15));
		AddProjectile ("Bullet", new Vector3(-15,15,15));
		AddProjectile ("Bullet", new Vector3(15,-15,15));
		AddProjectile ("Bullet", new Vector3(15,15,-15));
		*/
	}
}
