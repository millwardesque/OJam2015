using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public int damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		GameObject other = col.collider.gameObject;
		Health otherHealth = other.GetComponent<Health>();
		if (other.tag == "Player" && otherHealth != null) {
			otherHealth.AdjustHP(-damage);
		}
	}
}
