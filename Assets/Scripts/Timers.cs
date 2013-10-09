using UnityEngine;
using System.Collections;

public class Timers  {
	private Hashtable _timers = new Hashtable();
	
	private string[] _keys = new string[0];
	
	public void Set(string key, float val = 0f) {
		if (_timers.ContainsKey (key)) {
			_timers[key] = val;
		} else {
			_timers.Add (key, val);
		}
		//Creates Key Array
		_keys = new string[_timers.Count];
		int k = 0;
		foreach (DictionaryEntry entry in _timers){
			_keys[k] = (string)entry.Key;
			k++;
		}
	}
	
	public void Reset(string key) {
		Set (key, 0f);	
	}
	
	public float Get(string key) {
		if (!_timers.ContainsKey(key)) {
			Reset (key);
		}
		return (float) _timers[key];		
	}
	
	public bool IsZero(string key) {
		return Get (key) == 0;
	}
	
	// Update is called once per frame
	public void UpdateTimers (float deltaTime) {
		foreach (string key in _keys) {
			float val = Get (key);
			float delta = val - deltaTime;
			if (val > 0) {
				Set(key, delta);
			}
			if (val < 0) {
				Reset(key);
			}
		}
	}
	
}
