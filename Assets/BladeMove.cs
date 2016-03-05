using UnityEngine;
using System.Collections;

public class BladeMove : MonoBehaviour {
	
	private bool following;
	private float speed = 4f;
	private float horizontalSpeed = 2f;
	private bool canMove = true;
	private bool canDamage = true; 

	private bool isReversed;

	Camera camera;
	float shake = 0f;
	float shakeAmount = 0.01f;
	float decreaseFactor = 0.1f;

	bool hittingHero;

	void Start() {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (canDamage && collider.tag == "Hero" && collider.transform.position.y > transform.position.y) {
			collider.SendMessage("OnHit", gameObject);
			shakeAmount = 0.05f;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "Hero" && collider.transform.position.y > transform.position.y) {
			shakeAmount = 0.01f;
		}
	}

	void OnStatus(string status) {
		switch (status) {
			case "freeze":
				StartCoroutine("Freeze");
			break;
			case "reverse":
				horizontalSpeed = horizontalSpeed*-1;
			break;
		}
	}

	IEnumerator Freeze() {
		GetComponent<Animator>().SetInteger("state", 1);
		canMove = false;
		canDamage = false;

		yield return new WaitForSeconds(0.75f);

		GetComponent<Animator>().SetInteger("state", 0);
		canMove = true;
		canDamage = true;

		yield return null;
	}
	
	// Update is called once per frame
	void Update () {



		if (shake > 0) {
			camera.transform.localPosition = new Vector3 (3,-5,-10) + ( Random.insideUnitSphere * shakeAmount);
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0.0f;
		}


		Vector2 vel = new Vector2();

		if (following && canMove) {
			shake = hittingHero ? 2:1;

			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (pos.x > transform.localPosition.x) {
				vel.x = horizontalSpeed;
			}

			if (pos.x < transform.localPosition.x) {
				vel.x = -horizontalSpeed;
			}
		} else {
			shake = 0;
		}

		GetComponent<Rigidbody2D>().velocity = vel;

		if (Input.GetMouseButtonUp (0)) {
			following = false;
		}
	}

	void OnMouseDown () {
		following = true;
	}
}
