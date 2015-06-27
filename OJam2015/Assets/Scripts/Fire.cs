using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public int damage = 1;

	// Temporary hack to hide the flame until the collision detection processes whether a newly created flame was spawned on a wall.
	// To be replaced with a proper grid system soon.
	int fixedUpdateCount = 0;

	// Use this for initialization
	void Start () {
		gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fixedUpdateCount < 2) {
			fixedUpdateCount++;
		}
		if (fixedUpdateCount == 2) {	// Hide the renderer until Fixed Update process has run twice (to make sure the flame is still alive).
			fixedUpdateCount++;
			gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
		}

	}

	void OnCollisionEnter2D(Collision2D col) {
		GameObject other = col.collider.gameObject;

		if (other.tag == "Player") {
			Health otherHealth = other.GetComponent<Health>();
			if (otherHealth != null) {
				otherHealth.AdjustHP(-damage);
			}
		}
		else {
			Destroy (gameObject);
		}
	}
}
