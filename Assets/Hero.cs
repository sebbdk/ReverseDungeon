using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	private GameObject goal;
	private bool canMove;

	private float moveSpeed = 1f;

	public int health = 0;

	public GameObject[] hearts;

	// Use this for initialization
	void Start () {
		goal = GameObject.FindGameObjectWithTag ("Goal");
		renderHealth ();
	}

	void OnGameStart() {
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
			canMove = false;
			gameObject.SetActive (false);
			GameController.deathCount++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			Rigidbody2D body = GetComponent<Rigidbody2D> ();
			Vector2 vel = new Vector2(0, -moveSpeed);

			//dodge blades
			GameObject[] blades = GameObject.FindGameObjectsWithTag("Blade");

			GameObject closestBlade = blades[0];
			float closestDist = -1;
			foreach(GameObject blade in blades) {
				float dist = Vector2.Distance(transform.position, blade.transform.position);

				if(closestDist == -1 || dist < closestDist) {
					closestBlade = blade;
					closestDist = dist;
				}
			}

			if(closestDist != -1 && closestDist < 2) {
				if(closestBlade.transform.position.x > transform.position.x && transform.position.x > 0.5f) {
					vel.x = -1;
				} else if(transform.position.x < 5.5f) {
					vel.x = 1;
				}
			}

			//Move towards goal when at the bottom
			if(transform.position.y - 1 < goal.transform.position.y) {
				Vector2 dir = goal.transform.position - transform.position;
				vel = dir.normalized * moveSpeed;
			}

			//Stop if there
			if( Vector2.Distance(transform.position, goal.transform.position) < 0.5 ) {
				vel = new Vector2();
				GameController.instance.Lose();
			}

			//Move down otherwise.
			body.velocity = vel;
		}
	}
}
