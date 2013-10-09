using UnityEngine;
using System.Collections;

public class Pistol9mm : Weapon {
	public Pistol9mm () {
		name = "9mm Pistol";
		attackType = AttackType.Range;
		damage = 5;
		ammo = 20;
		maxAmmo = 200;
		coolDown = 2f;
		
		Projectile p = AddProjectile ("Bullet");
		p.blowback = 500;
	}	
}
