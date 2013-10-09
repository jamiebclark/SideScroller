using UnityEngine;
using System.Collections;

public class Enemy : BaseCharacter {
	public Transform target;
	public float targetDistance;
	
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	
	public int maxFollowDistance;
	public int minFollowDistance;

	
	// Use this for initialization
	public override void Start () {
		base.Start ();
		_myTransform = transform;	
		maxDistance = 20;
		
		maxFollowDistance = 20;
		minFollowDistance = 2;
		
		moveSpeed = 2;
		
		SetTarget();
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Debug.DrawLine(target.position, _myTransform.position, Color.green);
			targetDistance = Vector3.Distance(target.position, _myTransform.position);
			if (targetDistance <= maxFollowDistance && targetDistance >= minFollowDistance) {
				LookAt (target);
				_myTransform.position += _myTransform.forward * moveSpeed * Time.deltaTime;
			}
		}
	}
	
	private void SetTarget() {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}
}
