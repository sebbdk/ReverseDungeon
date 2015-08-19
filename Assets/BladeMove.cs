using UnityEngine;
using System.Collections;

public class BladeMove : MonoBehaviour {
	
	private bool following;
	private float speed = 2f;
	private float horizontalSpeed = 2f;
	private bool canMove = true;
	private bool canDamage = true; 

	private bool isReversed;

	void OnTriggerEnter2D(Collider2D collider) {
		if (canDamage && collider.tag == "Hero" && collider.transform.position.y > transform.position.y) {
			collider.SendMessage("OnHit", gameObject);
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
		Vector2 vel = new Vector2();

		if (following && canMove) {
			Vector3 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(pos.x > transform.localPosition.x) {
				vel.x = horizontalSpeed;
			}

			if(pos.x < transform.localPosition.x) {
				vel.x = -horizontalSpeed;
			}
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
