using UnityEngine;
using System.Collections;

//Action for phasing in and out tiles.

public class Phase : MonoBehaviour {
	public void PhaseIn() {
		if(gameObject.GetComponent<SpriteRenderer> ())
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		if(gameObject.GetComponent<Collider2D> ())
			gameObject.GetComponent<Collider2D> ().enabled = true;
		
	}
	
	public void PhaseOut() {
		if(gameObject.GetComponent<SpriteRenderer> ())
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		if(gameObject.GetComponent<Collider2D> ())
			gameObject.GetComponent<Collider2D> ().enabled = false;
	}
}
