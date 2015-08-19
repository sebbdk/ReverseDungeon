using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public static MusicController instance;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			print ("destroy extra sound...");
			GameObject.Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
		instance = this;

	}
}
