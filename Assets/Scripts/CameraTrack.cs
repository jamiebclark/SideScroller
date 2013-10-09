using UnityEngine;
using System.Collections;

public class CameraTrack : MonoBehaviour {
	public GameObject track;
	private Vector3 _initPosition;
	
	// Use this for initialization
	void Start () {
		_initPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = track.transform.position + _initPosition;
	}
}