using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Panel : MonoBehaviour {
	
	public enum TriggerType {
		Destroy,
		Phase
	}
	
	
	public enum EventType {
		Enter,
		Exit
	}
	
	public TriggerType type = TriggerType.Destroy;
	
	public List<GameObject> triggerObjects = new List<GameObject>();
	
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if (type == TriggerType.Destroy) {
			DestroyTrigger(collider2D.tag, EventType.Enter);
		}
		
		if (type == TriggerType.Phase) {
			PhaseTrigger(collider2D.tag, EventType.Enter);
		}
	}
	
	void OnTriggerExit2D(Collider2D collider2D) {
		if (type == TriggerType.Phase) {
			PhaseTrigger(collider2D.tag, EventType.Exit);
		}
	}
	
	void DestroyTrigger(string tag, EventType type) {
		if(tag == "Player" && type == EventType.Enter) {
			foreach(GameObject triggerObject in triggerObjects) {
				GameObject.Destroy(triggerObject);
			}
			
			triggerObjects.Clear();
		}
	}
	
	void PhaseTrigger(string tag, EventType type) {
		if(tag == "Player" && type == EventType.Enter) {
			foreach(GameObject triggerObject in triggerObjects) {
				triggerObject.BroadcastMessage("PhaseOut");
			}
		}
		
		if(tag == "Player" && type == EventType.Exit) {
			foreach(GameObject triggerObject in triggerObjects) {
				triggerObject.BroadcastMessage("PhaseIn");
			}
		}
	}
}
