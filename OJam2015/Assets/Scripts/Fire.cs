using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public int damage = 1;

	// Use this for initialization
	void Start () {
	}

	void Update() {
	}

	void OnCollisionEnter2D(Collision2D col) {
		GameObject other = col.collider.gameObject;

		if (other.tag == "Player") {
			Health otherHealth = other.GetComponent<Health>();
			if (otherHealth != null) {
				otherHealth.AdjustHP(-damage);
			}
		}
	}
}
