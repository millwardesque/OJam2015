using UnityEngine;
using System.Collections;

public class FireTrigger : MonoBehaviour {

	bool triggered = false;


	void OnTriggerEnter2D(Collider2D collider2D) {
		if (triggered) {
			return;
		}

		if (collider2D.tag == "Player") {
			gameObject.BroadcastMessage("LetsStartAFire");
			triggered = true;
		}
	}

}
