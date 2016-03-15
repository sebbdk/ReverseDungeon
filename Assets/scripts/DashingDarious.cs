using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashingDarious : Hero {

	Vector3 initialPosition;

	public List<GameObject> wave1;
	public List<GameObject> wave2;
	public List<GameObject> wave3;

	public List<List<GameObject>> waves = new List<List<GameObject>>();

	private int wave = 0;

	public void Start () {
		base.Start ();
		initialPosition = transform.position;

		waves.Add (wave1);
		waves.Add (wave2);
		waves.Add (wave3);
	}

	public void OnHit(GameObject theAssholeWhoHitMe) {
		base.OnHit (theAssholeWhoHitMe);
		Debug.Log ("Darious no like whirly things!!");

		if (health >= 0) {
			StartCoroutine("ResetAndSpawn");
		}

		if(health < 0) {
			StartCoroutine("SlowMoDeath");
		}
	}
	
	IEnumerator SlowMoDeath() {
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(1.5f);
		Time.timeScale = 1f;
		yield return null;
	}


	IEnumerator ResetAndSpawn() {
		canMove = false;
		iTween.MoveTo(gameObject, initialPosition, 1f);
		GetComponent<Animator> ().SetBool ("moving", false);

		foreach (GameObject minion in waves[wave]) {
			minion.SetActive (true);
			GameController.instance.heroCount++;
		}

		yield return new WaitForSeconds(1.5f);

		foreach (GameObject minion in waves[wave]) {
			minion.SendMessage ("OnGameStart");
		}

		wave++;

		yield return new WaitForSeconds(7);

		canMove = true;
		GetComponent<Animator> ().SetBool ("moving", true);


		yield return null;
	}
}
