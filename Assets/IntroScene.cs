using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("NextScene");
	}

	IEnumerator NextScene() {
		yield return new WaitForSeconds(3);
		Application.LoadLevel (Application.loadedLevel + 1);
		yield return null;
	}

}
