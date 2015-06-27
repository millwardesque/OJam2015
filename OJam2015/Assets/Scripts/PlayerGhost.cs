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

	void Update () {
		float x = -YourWorldOrgin.x + TheirWorldOrgin.x + OwnedPlayer.x;
		float y = -YourWorldOrgin.y + TheirWorldOrgin.y + OwnedPlayer.y;
		float z = OwnedPlayer.z;

		gameObject.transform.position = new Vector3 (x, y, z);
	}
}
