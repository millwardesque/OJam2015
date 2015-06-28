using UnityEngine;
using System.Collections;

public class FogOfWar : MonoBehaviour {
	
	SpriteRenderer renderer = null;
	
	bool isRevealed = false;
	
	void Start () {
		
		renderer = this.gameObject.GetComponent<SpriteRenderer> ();

		renderer.enabled = false;
		
	}

	float updateTime = 0.1f;
	
	void Update () {
		if (isRevealed) {
			return;
		}
		
		updateTime -= Time.deltaTime;
		
		if (updateTime > 0) {
			return;
		}
		
		updateTime = 0.1f;
		
		

		Vector3 usedPosition = Vector2.zero;
		
		if (Player.playerOne) {
			usedPosition = Player.playerOne.transform.position;
			CheckFogOfWar (usedPosition);
		}

		if (Player.playerTwo) {
			usedPosition = Player.playerTwo.transform.position;
			CheckFogOfWar (usedPosition);
		}

	}

	void CheckFogOfWar (Vector3 usedPosition)
	{
		Vector2 direction = usedPosition - gameObject.transform.position;
		if (Mathf.Sqrt (direction.x * direction.x + direction.y * direction.y) > 6) {
			return;
		}
		direction = direction.normalized;
		int mask = (1 << 8);
		RaycastHit2D hit = Physics2D.Raycast (gameObject.transform.position, direction, 6, mask);
		if (hit.collider != null) {
			if (hit.collider.tag == "Player") {
				renderer.enabled = true;
				isRevealed = true;
			}
		}
	}
}
