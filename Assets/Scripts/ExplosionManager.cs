using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ExplosionManager {
	public void AddExplosion(string owner, Vector3 pos, float dur, float scale = 1) {
		Debug.Log ("Adding Explosion");
		GameObject go = GameObject.Instantiate(Resources.Load("Explosion"), pos, Quaternion.identity) as GameObject;
		go.GetComponent<ExplosionObject>().Set(owner, dur, scale);
	}
}