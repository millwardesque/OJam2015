using UnityEngine;
using System.Collections;

//Action for ghosting tiles. Meant for sake of testing, and not gameplay

public class DebugGhostChildren : MonoBehaviour {	
	public void Start() {
		gameObject.BroadcastMessage ("GhostOut");
	}
}
