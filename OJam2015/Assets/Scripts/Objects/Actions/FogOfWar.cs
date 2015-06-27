using UnityEngine;
using System.Collections;

public class FogOfWar : MonoBehaviour {
	
	SpriteRenderer renderer = null;
	
	bool isRevealed = false;
	
	void Start () {

		renderer = this.gameObject.transform.parent.GetComponent<SpriteRenderer> ();
		
		renderer.color = Color.black;
		
	}
	
	// Update is called once per frame
	
	float updateTime = 0.1f;
	
	void Update () {

		//TODO Can't get this to work
		return;




		if (isRevealed) {
			return;
		}
		
		updateTime -= Time.deltaTime;
		
		if (updateTime > 0) {
			return;
		}
		
		updateTime = 0.1f;
		
		
		Vector2 direction = Vector2.one;
		
		if (Player.playerOne != null) {
			direction =  Player.playerOne.transform.position - gameObject.transform.position;
			
			if(Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y) > 8) {


				return;
			}

			//renderer.color = Color.gray;
			
			direction = direction.normalized;
			
			RaycastHit2D hit = Physics2D.Raycast (gameObject.transform.position, direction, 8);
			
			if (hit.collider != null) {
				if(hit.collider.tag == "PlayerVision") {
					renderer.color = Color.white;
					isRevealed = true;
				} else {
					Debug.Log(hit.collider.tag);
				}
			}
			
		}
	}
}
