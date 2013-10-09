using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseCharacter : MonoBehaviour {
	public JumpType jumpType = JumpType.Jump;
	public int speed = 5;
	public int jump = 300;
	public int health = 100;
	public int maxHealth = 100;
	public int weaponHeight = 1;
	public int coolDown = 0;
	
	private List<Weapon> _weapons;
	private Weapon _currentWeapon;
	
	// Protected values
	protected bool isWalking = false;
	protected bool isJumpingUp = false;
	protected bool isGrounded = true;

	private Vector3 _velocity;
	
	private float _jumpCoolDown = 0.1f;		//Time until next jump
	private float _lookDir;					//Direction along Horizontal axis the character is looking
	private Timers _timers = new Timers();	//Collection of all the timers
	private Transform _projectileSpawn;
	
	protected Transform _myTransform;
	protected Inventory _myInventory;
	
	#region Getters and Setters
	protected float LookDir {
		get {return _lookDir;}
		set {
			value = value < 0f ? -1f : 1f;
			if (value != _lookDir) {
				_lookDir = value;
				_myTransform.localEulerAngles = new Vector3(0,90 * _lookDir,0);
			}
		}
	}
	
	public int Health {
		get {return health;}
		set {
			if (value > maxHealth) {
				value = maxHealth;
			} else if (value < 0) {
				value = 0;
			}
			health = value;
		}
	}
	#endregion
	
	// Use this for initialization
	public virtual void Start () {
		_myTransform = transform;
		_myInventory = GetComponent<Inventory>();
		if (_myInventory != null && gameObject.tag != null) {
			_myInventory.owner = gameObject.tag;
		}
		
		_projectileSpawn = _myTransform.Find("ProjectileSpawn");
		_velocity = Vector3.zero;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		_timers.Set("attack");
		_timers.Set("jump");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_velocity.y = rigidbody.velocity.y;
		isWalking = (_velocity != Vector3.zero);
		isJumpingUp = (rigidbody.velocity.y > 0);
		isGrounded = IsGrounded ();
		
		rigidbody.velocity = _velocity;
		
		_timers.UpdateTimers(Time.deltaTime);
	}
	
	public void Attack() {
		Weapon currentWeapon = _myInventory.CurrentWeapon;
		if (currentWeapon != null) {
			string attackTimerIndex = "attack." + currentWeapon.name;
			if (_timers.IsZero(attackTimerIndex)) {
				currentWeapon.Attack (_projectileSpawn.position, _myTransform.rotation);
				_timers.Set(attackTimerIndex, coolDown + currentWeapon.coolDown);
			}
		}
	}
	
	/// <summary>
	/// Looks at target.
	/// </summary>
	/// <param name='target'>
	/// Target.
	/// </param>
	public void LookAt(Transform target) {
		LookDir = target.position.x < _myTransform.position.x ? -1 : 1;	
	}
	
	protected void MoveHorizontal(float axis) {
		_velocity.x = speed * axis;
		if (Math.Abs(_velocity.x) > .1) {
			LookDir = axis;
		}
	}
	
	protected void MoveVertical(float axis) {
		_velocity.z = speed * axis;
	}
	
	protected void Jump() {
		bool canJump = false;
		switch (jumpType) {
		case JumpType.Jump:
			canJump = isGrounded;
			break;
		case JumpType.DoubleJump:
			canJump = isGrounded || isJumpingUp;
			break;
		case JumpType.Fly:
			canJump = true;
			break;
		}
		Debug.Log (_timers.IsZero("jump"));
		Debug.Log (canJump);
		if (_timers.IsZero("jump") && canJump) {
			rigidbody.AddRelativeForce(new Vector3(0, jump, 0));
			_timers.Set("jump", _jumpCoolDown);
		}
	}
	
	private bool IsGrounded() {
		RaycastHit hit;
		bool cast = Physics.Raycast(_myTransform.position, -Vector3.up, out hit);
		return !cast || (hit.distance == 0);
	}	
}

public enum JumpType {
	Grounded,
	Jump,
	DoubleJump,
	Fly
}