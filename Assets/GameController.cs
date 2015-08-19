using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private GameObject[] heroes;

	public GameObject StartBtn;
	public GameObject WinBtn;
	public GameObject LoseBtn;

	public static int deathCount = 0;


	public static GameController instance;

	public string description = "Tresspassers...";

	// Use this for initialization
	void Start () {
		heroes = GameObject.FindGameObjectsWithTag ("Hero");

		Debug.Log ("Hero count!");
		Debug.Log (heroes.Length);

		deathCount = 0;
		instance = this;

		GameObject.Find ("LevelDescription").GetComponent<Text> ().text = description;
	}

	public void BeginGame() {
		StartBtn.SetActive (false);

		StartCoroutine("WaitThenStart");
	}

	IEnumerator WaitThenStart() {
		yield return new WaitForSeconds(4);

		foreach (GameObject hero in heroes) {
			hero.SendMessage("OnGameStart");
		}

		yield return null;
	}

	public void NextLevel() {
		if (Application.loadedLevel + 1 < Application.levelCount) {
			Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			Application.LoadLevel (1);
		}
	}

	public void Retry() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Lose() {
		if (!LoseBtn.activeSelf) {
			StartCoroutine("WaitThenShowGameOver");
		}
	}
	
	IEnumerator WaitThenShowGameOver() {
		yield return new WaitForSeconds(1);
		
		LoseBtn.SetActive (true);
		
		yield return null;
	}

	void Update () {
		if (deathCount >= heroes.Length) {
			WinBtn.SetActive (true);
		}
	}
}
