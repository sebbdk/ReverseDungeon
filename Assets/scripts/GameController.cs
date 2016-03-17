using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private GameObject[] heroes;

	public GameObject StartBtn;
	public GameObject WinBtn;
	public GameObject LoseBtn;
	public GameObject menu;

	public int heroCount = 0;

	public static int deathCount = 0;


	public static GameController instance;

	public string description = "Tresspassers...";

	private bool animatingMenu;

	public static bool paused;

	// Use this for initialization
	void Start () {
		heroes = GameObject.FindGameObjectsWithTag ("Hero");

		Debug.Log ("Hero count!");
		Debug.Log (heroes.Length);

		heroCount = heroes.Length;

		deathCount = 0;
		instance = this;

		GameObject.Find ("LevelDescription").GetComponent<Text> ().text = description;
	}

	public void toggleSound() {
		if(!AudioListener.pause) {
			AudioListener.pause = true;
			AudioListener.volume = 0;
			GameObject.Find ("audioToggleBtn").GetComponent<Text> ().text = "Toggle sound on";
		} else {
			AudioListener.pause = false;
			AudioListener.volume = 1;
			GameObject.Find ("audioToggleBtn").GetComponent<Text> ().text = "Toggle sound off";
		}
	}

	public void BeginGame() {
		StartBtn.GetComponent<Animator> ().SetBool ("triggered", true);

		StartCoroutine("WaitThenStart");
	}

	public void ToggleMenu() {
		if (!animatingMenu) {
			animatingMenu = !animatingMenu;
			StartCoroutine ("toggleMenuTiming");
			SebbTrack.Track ("menu_toggle", 1);
		}
	}

	IEnumerator toggleMenuTiming() {
		if (menu.activeSelf) {
			menu.GetComponent<Animator> ().Play ("HideUp");
			yield return new WaitForSeconds(1);
		}

		menu.SetActive (!menu.activeSelf);
		paused = menu.activeSelf;


		animatingMenu = false;

		yield return null;
	}

	IEnumerator WaitThenStart() {
		yield return new WaitForSeconds(1);
		StartBtn.SetActive (false);
		yield return new WaitForSeconds(3);


		foreach (GameObject hero in heroes) {
			hero.SendMessage("OnGameStart");
		}

		yield return null;
	}

	public void NextLevel() {
		SebbTrack.Track ("nextLevel", 1);
		if (Application.loadedLevel + 1 < Application.levelCount) {
			Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			SebbTrack.Track ("completePlay", 1);
			Application.LoadLevel (1);
		}
	}

	public void Retry() {
		SebbTrack.Track ("retry", 1);
		Application.LoadLevel(Application.loadedLevel);
	}

	public void goHome() {
		Application.LoadLevel(0);
		SebbTrack.Track ("goHome", 1);
	}

	public void Lose() {
		if (!LoseBtn.activeSelf) {
			SebbTrack.Track ("lose", 1);
			StartCoroutine("WaitThenShowGameOver");
		}
	}
	
	IEnumerator WaitThenShowGameOver() {
		yield return new WaitForSeconds(1);
		LoseBtn.SetActive (true);
		yield return null;
	}

	void FixedUpdate () {
		if (deathCount >= heroCount && !WinBtn.activeSelf) {
			WinBtn.SetActive (true);
			SebbTrack.Track ("win", 1);
		}
	}
}
