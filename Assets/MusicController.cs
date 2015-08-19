using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public static MusicController instance;

	public AudioClip menuMusic;
	public AudioClip creditsMusic;
	private AudioClip defaultMusic;

	private AudioSource audio;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			print ("destroyed extra sound...");
			GameObject.Destroy(gameObject);
			return;
		}

		audio = GetComponent<AudioSource> ();

		defaultMusic = audio.clip;

		updateMusicChoice ();

		DontDestroyOnLoad (gameObject);
		instance = this;

	}

	void OnLevelWasLoaded () {
		if (defaultMusic) {
			updateMusicChoice ();
		}
	}

	void updateMusicChoice() {
		AudioClip clip = audio.clip;

		switch (Application.loadedLevelName) {
			case "Home":
				if(menuMusic != null) {
					clip = menuMusic;
				}
				break;
			case "credits":
				if(creditsMusic != null) {
					clip = creditsMusic;
				}
				break;
			default:
				if(defaultMusic != null) {
					clip = defaultMusic;
				}
				break;
		}

		if (clip != audio.clip) {
			audio.clip = clip;
			audio.Play ();
		}
	}

}
