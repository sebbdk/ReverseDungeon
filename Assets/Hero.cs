using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	private GameObject goal;
	private bool canMove;

	private float moveSpeed = 1f;

	public int health = 0;

	public GameObject[] hearts;

	private AudioSource audio;

	public AudioClip hurtSound;

	// Use this for initialization
	void Start () {
		goal = GameObject.FindGameObjectWithTag ("Goal");
		renderHealth ();

		if (hurtSound != null) {
			audio = gameObject.AddComponent<AudioSource> ();
			audio.clip = hurtSound;
		}
	}

	void OnGameStart() {
		print ("start!!!!");
		canMove = true;
	}

	void renderHealth() {
		foreach (GameObject heart in hearts) {
			heart.SetActive(false);
		}

		for (int x = health; x >= 0; x--) {
			hearts[x].SetActive(true);
		}
	}

	void OnHit(GameObject theAssholeWhoHitMe) {
		health--;

		renderHealth ();

		if (health < 0) {
			audio.Play ();
			GetComponent<Animator>().SetBool("is_dead", true);

			canMove = false;
			StartCoroutine("WaitThenCleanup");
		}
	}
	
	IEnumerator WaitThenCleanup() {
		yield return new WaitForSeconds(2);
		GameController.deathCount++;

		print ("not active!!");
		//gameObject.SetActive (false);
		
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		if (canMove) {
			Vector2 vel = new Vector2 (0, -moveSpeed);

			//dodge blades
			GameObject[] blades = GameObject.FindGameObjectsWithTag ("Blade");

			GameObject closestBlade = blades [0];
			float closestDist = -1;
			foreach (GameObject blade in blades) {
				float dist = Vector2.Distance (transform.position, blade.transform.position);

				if (blade.transform.position.y > transform.position.y) {
					continue;
				}

				if (closestDist == -1 || dist < closestDist) {
					closestBlade = blade;
					closestDist = dist;
				}
			}

			if (closestDist != -1 && closestDist < 2) {
				if (closestBlade.transform.position.x > transform.position.x && transform.position.x > 0.5f) {
					vel.x = -1;
				} else if (transform.position.x < 5.5f) {
					vel.x = 1;
				}
			}

			//Move towards goal when at the bottom
			if (transform.position.y - 1 < goal.transform.position.y) {
				Vector2 dir = goal.transform.position - transform.position;
				vel = dir.normalized * moveSpeed;
			}

			//Stop if there
			if (Vector2.Distance (transform.position, goal.transform.position) < 0.5) {
				vel = new Vector2 ();
				GameController.instance.Lose ();
			}

			//Move down otherwise.
			body.velocity = vel;
		} else {
			body.velocity = new Vector2();
		}
	}
}
