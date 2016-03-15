using UnityEngine;
using System.Collections;

public class ReverseStatus : MonoBehaviour {

	void Start() {
		//reverse ();
	}


	private int count = 1;
	
	void OnHit(GameObject theAssholeWhoHitMe) {
		reverse ();
	}

	void reverse() {
		if (count > 0) {
			GameObject[] assholes = GameObject.FindGameObjectsWithTag ("Blade");
			
			foreach (GameObject hole in assholes) {
				hole.SendMessage ("OnStatus", "reverse");
			}
			
			count--;
		}
	}

}
