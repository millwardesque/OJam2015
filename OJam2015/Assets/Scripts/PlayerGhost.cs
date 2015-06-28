using UnityEngine;
using System.Collections;

public class PlayerGhost : MonoBehaviour {
	public GameObject yourWorldOrgin = null;

	public Vector3 YourWorldOrgin {
		get {
			if(yourWorldOrgin) {
				return yourWorldOrgin.transform.position;
			}

			return Vector3.zero;
		}
	}

	public GameObject theirWorldOrgin = null;

	public Vector3 TheirWorldOrgin {
		get {
			if(theirWorldOrgin) {
				return theirWorldOrgin.transform.position;
			}
			
			return Vector3.zero;
		}
	}

	public GameObject ownedPlayer = null;

	public Vector3 OwnedPlayer {
		get {
			if(ownedPlayer) {
				return ownedPlayer.transform.position;
			}
			
			return Vector3.zero;
		}
	}

	public GameObject hauntedPlayer = null;
	
	public Vector3 HauntedPlayer {
		get {
			if(ownedPlayer) {
				return hauntedPlayer.transform.position;
			}
			
			return Vector3.zero;
		}
	}

	void UpdateGhostPosition (ref Vector3 ghostPosition)
	{
		float x = -YourWorldOrgin.x + TheirWorldOrgin.x + OwnedPlayer.x;
		float y = -YourWorldOrgin.y + TheirWorldOrgin.y + OwnedPlayer.y;
		float z = OwnedPlayer.z;
		ghostPosition = new Vector3 (x, y, z);
		gameObject.transform.position = ghostPosition;
	}

	float strayTooFarRadiusOfTerror = 7.5f;
	float returnToGroup = 5.25f;
	bool isTooFar = false;

	void Update () {
		Vector3 ghostPosition = Vector3.zero;
		UpdateGhostPosition (ref ghostPosition);

		float x = HauntedPlayer.x - ghostPosition.x;
		float y = HauntedPlayer.y - ghostPosition.y;

		if (Mathf.Sqrt (x * x + y * y) > strayTooFarRadiusOfTerror) {
			if(!isTooFar) {
				isTooFar = true;

				StrayTooFar ();
			}
		}

		if (Mathf.Sqrt (x * x + y * y) < returnToGroup) {
			if(isTooFar) {
				isTooFar = false;
				
				BackInRange ();
			}
		}

	}

	void StrayTooFar () {
		string message = "We're too far apart!";
		if (hauntedPlayer.GetComponent<Player>().type == Player.PlayerType.One) {
			SpeechBubbleManager.Instance.SetPlayer1Message(message);
		}
		else {
			SpeechBubbleManager.Instance.SetPlayer2Message(message);
		}
	}

	void BackInRange () {
		if(SpeechBubbleManager.Instance)
		SpeechBubbleManager.Instance.DisablePlayer1Bubble();
		if(SpeechBubbleManager.Instance)
		SpeechBubbleManager.Instance.DisablePlayer2Bubble();
	}
}
