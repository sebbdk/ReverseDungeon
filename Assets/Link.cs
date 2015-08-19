using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public string scene;

	public void Go () {
		Application.LoadLevel (scene);
	}
}
