using UnityEngine;
using System.Collections;

public class ProjectileObject : MonoBehaviour {
	private float _timer = 0;
	
	public Projectile projectile;
	
	private ExplosionManager _explosionManager;
	
	// Use this for initialization
	void Start () {
		_explosionManager = new ExplosionManager();
		
		//transform.localScale = new Vector3(scale, scale, scale);
		gameObject.AddComponent<MeshCollider>();
		gameObject.AddComponent<DontGoThroughThings>();

		
		switch(projectile.projectileType){
		case ProjectileType.Lob:
			transform.Rotate (new Vector3(-45,0,0));
			rigidbody.AddRelativeForce(Vector3.forward * 500);
			rigidbody.useGravity = true;
			break;
		case ProjectileType.Straight:
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			rigidbody.useGravity = false;
			break;
		}
	}
	
	void Update() {
		if (projectile.projectileType == ProjectileType.Straight) {
			transform.position += transform.forward * projectile.speed * Time.deltaTime;
		}
		_timer += Time.deltaTime;
		if (_timer >= projectile.lifeSpan) {
			projectile.DestroyProjectile();
		}
	}
	
	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == projectile.EnemyTag) {
			BaseCharacter bc = hit.gameObject.GetComponent<BaseCharacter>();
			if (bc != null) {
				projectile.Hit(bc);
			}
		}
	}
	
	public void DestroyProjectile() {
		Destroy(gameObject);
	}
}
