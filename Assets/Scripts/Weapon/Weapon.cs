using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon  {
	public string name;
	public int ammo = 0;
	public int maxAmmo = 100;
	public int ammoPerShot = 1;
	public int damage = 5;
	
	public string owner;		//Tag of the GameObject using this
	
	public bool infiniteAmmo = false;
	public AttackType attackType = AttackType.Melee;
	
	public float coolDown = 1f;		//Time before weapon is ready again (in seconds)
	public float meleeRange = 1f;	//Range of melee attack swing (in units)
	
	private Vector3 _position;
	private Quaternion _rotation;
	private bool _attacking = false;
	
	//private ProjectileManager _projectileManager;
	protected List<Projectile> _projectiles = new List<Projectile>();

	public Weapon() {
		//_projectileManager = GameObject.Find("_ProjectileManager").GetComponent<ProjectileManager>();
	}
	
	public void AddAmmo(int ammoToAdd) {
		ammo += ammoToAdd;
		if (maxAmmo > 0 && ammo > maxAmmo) {
			ammo = maxAmmo;
		}
	}
	
	protected Projectile AddProjectile (string resourceName, Vector3 setAngle = default(Vector3), int setDamage = 0) {
		Projectile p = new Projectile(resourceName);
		if (setAngle != null) {
			p.angle = setAngle;
		}
		if (setDamage > 0) {
			p.damage = setDamage;
		}
		_projectiles.Add(p);
		return p;
	}

	public void Attack(Vector3 position, Quaternion rotation) {
		_position = position;
		_rotation = rotation;
		if (!_attacking) {
			//Checks if weapon is still recharging from last fire
			switch(attackType){
			case AttackType.Melee:
				MeleeAttack();
				break;
			case AttackType.Range:
				RangeAttack();
				break;
			case AttackType.Perimeter:
				PerimeterAttack();
				break;
			}
		}
	}

	private void MeleeAttack() {
		Debug.Log ("Melee Attack");
	}
	
	private void RangeAttack() {
		if (!infiniteAmmo && ammo <= 0) {
			Debug.Log ("Out of ammo");
		} else {
			_attacking = true;
			foreach (Projectile p in _projectiles) {
				p.Fire(owner, _position, _rotation);
			}
			_attacking = false;
			
			if (!infiniteAmmo) {
				ammo -= ammoPerShot;
				if (ammo < 0) {
					ammo = 0;
				}
			}
			Debug.Log ("Range Attack: Ammo left: " + ammo);
		}
	}
	
	private void PerimeterAttack() {
		Debug.Log ("PerimeterAttack Attack");	
	}	
}

public enum AttackType {
	Melee,
	Range,
	Toss,
	Perimeter
}
public struct ProjectileVars {
	public string resourceName;
	public Vector3 rotation;
	public float delay;
	public bool toss;
	
	public  ProjectileVars(string n) {
		resourceName = n;
		rotation = Vector2.zero;
		delay = 0f;
		toss = false;
	}
}

public struct ExplosionVars {
	public float duration;
	public float scale;
	
	public ExplosionVars(float d, float s) {
		duration = d;
		scale = s;		
	}
}