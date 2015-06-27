using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rb;
	public float speed = 1f;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		float x = rb.velocity.x;
		float y = rb.velocity.y;

		if (rb.velocity.magnitude < 0.2f) {
			GetComponentInChildren<Animator>().speed = 0f;
			// GetComponentInChildren<Animator>().SetTrigger("Idle");
		}
		else {
			GetComponentInChildren<Animator>().speed = 1f;
			if (Mathf.Abs(x) > Mathf.Abs (y)) {
				if (x > 0) {
					GetComponentInChildren<Animator>().SetTrigger("Walking Right");
				}
				else if (x < 0) {
					GetComponentInChildren<Animator>().SetTrigger("Walking Left");
				}
			}
			else {
				if (y > 0) {
					GetComponentInChildren<Animator>().SetTrigger("Walking Up");
				}
				else if (y < 0) {
					GetComponentInChildren<Animator>().SetTrigger("Walking Down");
				}
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis ("Vertical");
		Vector2 force = new Vector2(x, y) * speed;

		this.rb.AddForce(force);
	}
}
