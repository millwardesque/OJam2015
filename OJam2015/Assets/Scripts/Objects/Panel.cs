using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Panel : MonoBehaviour {
	
	public enum TriggerType {
		Destroy,
		Phase
	}

	
	public TriggerType type = TriggerType.Destroy;
	
	public List<GameObject> triggerObjects = new List<GameObject>();
	
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if (type == TriggerType.Destroy) {
			DestroyTrigger(collider2D.tag, Globals.EventType.Enter);
		}
		
		if (type == TriggerType.Phase) {
			PhaseTrigger(collider2D.tag, Globals.EventType.Enter);
		}
	}
	
	void OnTriggerExit2D(Collider2D collider2D) {
		if (type == TriggerType.Phase) {
			PhaseTrigger(collider2D.tag, Globals.EventType.Exit);
		}
	}
	
	void DestroyTrigger(string tag, Globals.EventType type) {
		if(tag == "Player" && type == Globals.EventType.Enter) {
			foreach(GameObject triggerObject in triggerObjects) {
				GameObject.Destroy(triggerObject);
			}

			if(GetComponentInChildren<SpriteRenderer>()) {
				var thisScale = GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale;

				GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(-thisScale.x, -thisScale.y, thisScale.z);
			}
			
			triggerObjects.Clear();
		}
	}
	
	void PhaseTrigger(string tag, Globals.EventType type) {
		if(tag == "Player" && type == Globals.EventType.Enter) {
			foreach(GameObject triggerObject in triggerObjects) {
				triggerObject.BroadcastMessage("PhaseOut");
			}
		}
		
		if(tag == "Player" && type == Globals.EventType.Exit) {
			foreach(GameObject triggerObject in triggerObjects) {
				triggerObject.BroadcastMessage("PhaseIn");
			}
		}
	}
}
