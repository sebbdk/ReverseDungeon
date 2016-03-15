using UnityEngine;
using System.Collections;

public class FreezeOnHit : MonoBehaviour {

	private int count = 1;

	void OnHit(GameObject theAssholeWhoHitMe) {

		if (count > 0) {
			theAssholeWhoHitMe.SendMessage ("OnStatus", "freeze");
			count--; 
		}

	}

}
