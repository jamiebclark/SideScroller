using UnityEngine;
using System.Collections;

public class Grenade : Weapon {
	public Grenade() {
		name = "Grenade";
		coolDown = 2f;
		attackType = AttackType.Range;
		ammo = 5;
		maxAmmo = 12;
		
		Projectile p = AddProjectile("Grenade");
		p.projectileType = ProjectileType.Lob;
		p.lifeSpan = 5f;
		
		p.OnDestroyExplosion = new ExplosionVars(3f,4f);
	}
}
