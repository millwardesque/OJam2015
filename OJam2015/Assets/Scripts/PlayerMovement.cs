using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rigidbody;
	public float speed = 1f;

	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis ("Vertical");
		Vector2 force = new Vector2(x, y) * speed;

		this.rigidbody.AddForce(force);
	}
}
