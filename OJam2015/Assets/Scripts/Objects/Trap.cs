using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public enum TrapType {
		Spikes
	}

	public TrapType type = TrapType.Spikes;

	void OnTriggerEnter2D(Collider2D collider2D) {
		if (type == TrapType.Spikes) {
			SpikeTrap(collider2D.tag, collider2D, Globals.EventType.Enter);
		}

	}
	
	void OnTriggerExit2D(Collider2D collider2D) {
		if (type == TrapType.Spikes) {
			SpikeTrap(collider2D.tag, collider2D, Globals.EventType.Exit);
		}
	}

	private void SpikeTrap(string tag, Collider2D collider2D, Globals.EventType type) {
		if(tag == "Player" && type == Globals.EventType.Enter) {
			if(collider2D.GetComponent<PlayerHealth>()) {
				collider2D.GetComponent<PlayerHealth>().AdjustHP(-1);
			}
		}
	}
}
