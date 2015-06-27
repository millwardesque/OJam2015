using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Panel : MonoBehaviour {
	
	public enum TriggerType {
		Destroy
	}
	
	public TriggerType type = TriggerType.Destroy;
	
	public List<GameObject> triggerObjects = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if (type == TriggerType.Destroy) {
			if (collider2D.gameObject.tag == "Player") {
				foreach(GameObject triggerObject in triggerObjects) {
					GameObject.Destroy(triggerObject);
				}
				
				triggerObjects.Clear();
			}
		}
	}
}
