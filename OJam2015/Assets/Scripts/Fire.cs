using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public int damage = 1;
	public float spawnProbability = 0.01f; // Probability at which new flames are spawned

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
