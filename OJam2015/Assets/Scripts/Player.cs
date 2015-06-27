using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public static Player playerOne = null;

	public enum PlayerType {
		One,
		Two
	}

	public PlayerType type = PlayerType.One;

	void Start () {
		if (type == PlayerType.One) {
			playerOne = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
