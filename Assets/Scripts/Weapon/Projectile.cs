/**
 * Some Ideas:
 * After the BergZurg ideas, take a look at Projectile and Explosions
 * Start with a basic ExplosionMod class that's not tied to MonoBehavior
 * That gives you a base to work from in all of your objects
 * 
 * Also think towards buff classes. 
 * Could you pick up something that adds +5 to damage in all your guns?
 * In just your active gun?
 * Something that adds an ImpactExplosion to your bullets? (All guns? Only some guns? Maybe special bullets?)
 * 
 * 
 * */
using UnityEngine;
using System.Collections;

public class Projectile {
	public string resourceName = "Bullet";
	
	public float speed = 50f;
	public Vector3 angle;
	public float scale = 1f;
	public float lifeSpan = 1f;		//Time it exists (in seconds)
	public ProjectileType projectileType = ProjectileType.Straight;
	public int damage = 20;
	public int blowback = 100;
	
	public ExplosionVars OnDestroyExplosion;
	public ExplosionVars OnImpactExplosion;
	
	private string _ownerTag;		//Tag of GameObjects to not receive damage
	private string _enemyTag;		//Tag of GameObjects to receive damage
	
	private ProjectileObject _projectileObject;
	private ExplosionManager _explosionManager;
	
	public Projectile(string setResourceName) {
		resourceName = setResourceName;
		_explosionManager = new ExplosionManager();
	}
	
	#region getters and setters
	public string OwnerTag {
		get {
			return _ownerTag;
		}
		set {
			_ownerTag = value;
			_enemyTag = value == "Enemy" ? "Player" : "Enemy";
		}
	}
	public string EnemyTag {
		get {return _enemyTag;}
	}
	#endregion
	
	public void Fire(string owner, Vector3 position, Quaternion rotation) {
		OwnerTag = owner;
		GameObject go = GameObject.Instantiate(Resources.Load(resourceName), position, rotation) as GameObject;
		_projectileObject = go.GetComponent<ProjectileObject>();
		_projectileObject.projectile = this;
		
		Debug.Log (angle);
		if (angle != Vector3.zero) {
			_projectileObject.transform.Rotate(angle);
		}
	}
	
	public void Hit(BaseCharacter target){
		Debug.Log (target.name + " Hit for " + damage + " hit points. Current HP: " + target.Health);
		target.Health -= damage;
		DestroyProjectile();
		if (OnImpactExplosion.scale != null) {
			AddExplosion(OnImpactExplosion);
		}
		if (blowback != null){
			target.GetComponent<Rigidbody>().AddForce(_projectileObject.GetComponent<Transform>().forward * blowback);
		}
	}
	
	public void DestroyProjectile() {
		Debug.Log (OnDestroyExplosion);
		if (OnDestroyExplosion.scale != null) {
			AddExplosion (OnDestroyExplosion);
		}
		_projectileObject.DestroyProjectile();
	}
	
	
	private void AddExplosion(ExplosionVars explosionVars){
		AddExplosion (explosionVars, _projectileObject.transform.position);
	}
	
	private void AddExplosion(ExplosionVars explosionVars, Vector3 position) {
		_explosionManager.AddExplosion(
			OwnerTag,
			position,
			explosionVars.duration,
			explosionVars.scale
		);	
	}
}

public enum ProjectileType {
	Straight,
	Spread,
	Lob
}