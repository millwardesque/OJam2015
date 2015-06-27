using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player") {
			Destroy(col.collider.gameObject);
		}
	}
}
