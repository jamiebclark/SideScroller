using UnityEngine;
using System.Collections;

public class Player : BaseCharacter {
	public bool _jumpPressed = false;
	public float yVelocity;
	
	public override void Start() {
		base.Start ();
		//Weapon initWeapon = ;
		_myInventory = GetComponent<Inventory>();
		
		_myInventory.AddWeapon (new Pistol9mm());
		_myInventory.AddWeapon (new MachineGun());
		_myInventory.AddWeapon (new Shotgun());
		_myInventory.AddWeapon (new Grenade());
	}
	
	void Update() {
		yVelocity = rigidbody.velocity.y;
		MoveHorizontal(Input.GetAxis("Horizontal"));
		MoveVertical(Input.GetAxis("Vertical"));
		if (Input.GetButton ("Jump")) {
			if (isGrounded || !_jumpPressed) {
				_jumpPressed = true;
				Jump();
			}
		} else if (_jumpPressed) {
			_jumpPressed = false;
		}
		if (Input.GetButton ("Fire1")){
			Attack();
		} 
		
		if (Input.GetButtonDown ("NextWeapon")) {
			_myInventory.NextWeapon ();
		}
	}
}
