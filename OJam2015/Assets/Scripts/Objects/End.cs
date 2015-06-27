using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	public GameObject endPoint;
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if(collider2D.tag == "Player" && endPoint) {
			collider2D.gameObject.transform.position = endPoint.transform.position;
		}
	}
}
