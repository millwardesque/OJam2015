using UnityEngine;
using System.Collections;

//Action for ghosting tiles. Meant for sake of testing, and not gameplay

public class DebugGhostChildren : MonoBehaviour {
	public enum DebugGhostType {
		NoCollision,
		SeeThrough
	}

	public DebugGhostType type = DebugGhostType.SeeThrough;

	public void Start() {
		if (type == DebugGhostType.NoCollision) {
			gameObject.BroadcastMessage ("GhostOut");
		} 
		if (type == DebugGhostType.SeeThrough) {
			gameObject.BroadcastMessage ("SeeThroughOut");

		}

	}
}
