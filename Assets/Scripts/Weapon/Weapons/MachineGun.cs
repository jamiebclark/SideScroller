using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {
	public MachineGun() {
		name = "Machine Gun";
		attackType = AttackType.Range;
		damage = 5;
		ammo = 100;
		maxAmmo = 500;
		coolDown = 0.1f;
		AddProjectile ("Bullet");
	}	
}