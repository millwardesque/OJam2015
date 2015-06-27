using UnityEngine;
using System.Collections;

//Action for debug ghosting tiles. For editing layered levels

public class DebugGhost : MonoBehaviour {
	public void GhostOut() {
		if (gameObject.GetComponent<SpriteRenderer> ())
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (gameObject.GetComponent<SpriteRenderer> ().color.r, 
			                                                              gameObject.GetComponent<SpriteRenderer> ().color.g, 
			                                                              gameObject.GetComponent<SpriteRenderer> ().color.b, 
			                                                              0.2f); 
		if(gameObject.GetComponent<Collider2D> ())
			gameObject.GetComponent<Collider2D> ().enabled = false;
		
	}
}
