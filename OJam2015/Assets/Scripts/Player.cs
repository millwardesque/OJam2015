using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public static Player playerOne = null;
	public static Player playerTwo = null;


	public enum PlayerType {
		One,
		Two
	}

	public PlayerType type = PlayerType.One;


	void Start () {
		if (type == PlayerType.One) {
			playerOne = this;
		}

		if (type == PlayerType.Two) {
			playerTwo = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
