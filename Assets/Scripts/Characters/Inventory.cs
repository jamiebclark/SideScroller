using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Weapon> Weapons = new List<Weapon>();
	public string owner;		//Tag of the GameObject using this
	
	private Weapon _currentWeapon;
	private int _currentWeaponIndex;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Weapon CurrentWeapon {
		get {return _currentWeapon;}
		set {
			int index = FindWeaponIndex (value);
			if (index != null) {
				_currentWeapon = Weapons[index];
				_currentWeaponIndex = index;
				Debug.Log ("Current Weapon Set To: " + _currentWeapon.name);
			}			
		}
	}
	
	public void NextWeapon() {
		int index;
		if (_currentWeaponIndex != null) {
			index = _currentWeaponIndex + 1;
		}
		if (index >= Weapons.Count) {
			index = 0;
		}		
		Debug.Log ("Current Weapon Count: " + Weapons.Count + ", New Index: " + index);
		if (index != null) {
			CurrentWeapon = Weapons[index];
		}
	}
	
	/// <summary>
	/// Adds the weapon to the inventory List.
	/// </summary>
	/// <param name='weapon'>
	/// Weapon.
	/// </param>
	public void AddWeapon(Weapon weapon) {
		Weapon result = FindWeapon (weapon);
		if (result != null) {
			result.AddAmmo(weapon.ammo);	
		} else {
			weapon.owner = owner;
			Weapons.Add (weapon);
		}
		CurrentWeapon = weapon;
		Debug.Log ("Added Weapon: " + weapon.name + ", Current COunt: " + Weapons.Count);
	}
	
	/// <summary>
	/// Removes the weapon from the inventory List.
	/// </summary>
	/// <param name='weapon'>
	/// Weapon.
	/// </param>
	public void RemoveWeapon(Weapon weapon) {
		Weapons.Remove(weapon);
	}
	
	public void UnsetCurrentWeapon() {
		CurrentWeapon = null;
	}
	
	private int FindWeaponIndex(Weapon weapon) {
		return Weapons.FindIndex(w => w.name == weapon.name);
	}
			
	private Weapon FindWeapon(Weapon weapon) {
		int index = FindWeaponIndex(weapon);
		if (index >= 0) {
			return Weapons[index];
		} else {
			return null;
		}
	}
}
