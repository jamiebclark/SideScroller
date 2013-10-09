using UnityEngine;
using System.Collections;

public class ExplosionObject : MonoBehaviour {
	//public Explosion explosion;
	public string owner;
	public float radius;
	public float duration;
	public float scale = 1;
	
	private float _timer;
	private float _localScale;
	private float _explosionGrow;
	
	private GameObject _smallExplosion;
	
	private ParticleAnimator _particleAnimator;

	
	#region getters and setters
	public float Duration {
		get{return duration;}
		set{
			LocalScale = 0;
			duration = value;
			_timer = value;
		}
	}

	public float Timer {
		get{return _timer;}
		set{
			if (value < 0) {
				value = 0;
			}
			_timer = value;
		}
	}
	
	public float Scale {
		get{return scale;}
		set{
			transform.localScale = new Vector3(value, value, value);
			scale = value;
			LocalScale = 1;
			_explosionGrow = 50 * value - 1;
		}
	}
	
	public float LocalScale {
		get {return _localScale;}
		set {
			_localScale = value;
			value *= scale;
			transform.localScale = new Vector3(value, value, value);
			if (_particleAnimator != null) {
				_particleAnimator.sizeGrow = value * _explosionGrow;
			} else {
				Debug.Log ("NO PARTICLE ANIMATOR");
			}
		}
	}
	
	#endregion
	
	// Use this for initialization
	void Awake () {
		Duration = 0f;
		Timer = 0f;
		LocalScale = 0f;
		_particleAnimator = transform.GetComponentInChildren<ParticleAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Timer > 0 && Duration > 0) {
			Timer -= Time.deltaTime;
			float timerPct = Timer / Duration;
			if (timerPct > .5) {
				LocalScale = (float)((1 - timerPct) / .5);
			} else {
				LocalScale = (float)(timerPct / .5);
			}
		} else {
			Destroy (gameObject);
		}
	}
	
	public void Set(string owner, float duration, float scale){
		owner = owner;
		Duration = duration;
		Scale = scale;		
		LocalScale = 0;
	}
}