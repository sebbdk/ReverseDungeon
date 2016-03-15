using UnityEngine;
using System.Collections;

public class DashingDarious : Hero {

	Vector3 initialPosition;

	public void Start () {
		base.Start ();
		initialPosition = transform.position;
	}

	public void OnHit(GameObject theAssholeWhoHitMe) {
		base.OnHit (theAssholeWhoHitMe);
		Debug.Log ("Darious no like whirly things!!");

	//	transform.position = initialPosition;

		if (health >= 0) {
			StartCoroutine("ResetAndSpawn");
		}
	}

	IEnumerator ResetAndSpawn() {
		canMove = false;
		iTween.MoveTo(gameObject, initialPosition, 1f);
		GetComponent<Animator> ().SetBool ("moving", false);

		yield return new WaitForSeconds(5);

		canMove = true;
		GetComponent<Animator> ().SetBool ("moving", true);

		yield return null;
	}
}
