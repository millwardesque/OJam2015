using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Panel : MonoBehaviour {
	
	public enum TriggerType {
		Destroy
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
	}
	
	void OnTriggerExited2D(Collider2D collider2D) {
		
	}
	
	void DestroyTrigger(string tag, EventType type) {
		if(tag == "Player" && type == EventType.Enter) {
			foreach(GameObject triggerObject in triggerObjects) {
				GameObject.Destroy(triggerObject);
			}
			
			triggerObjects.Clear();
		}
	}
}
