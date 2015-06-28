using UnityEngine;
using System.Collections;

public class CanvasInstance : MonoBehaviour {
	void Start () {
		if (instance && instance.gameObject) {
			Destroy(gameObject);
			return;
		}
		
		instance = this;
	}
	
	public static CanvasInstance instance = null;

}
