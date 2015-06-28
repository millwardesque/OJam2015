using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeAsset : MonoBehaviour {
	public GameObject black = null;
	public GameObject wall = null;
	public GameObject corner = null;
	
	bool once = true;
	public void OnDrawGizmos() {
		Change ();
	}
	
	List<int> coreDirections = new List<int>();
	
	void safeAdd(int direction) {
		if (!coreDirections.Contains (direction)) {
			coreDirections.Add(direction);
		}
	}
	
	void Change () {
		if (wall == null || corner == null || black == null) {
			return;
		}
		
		var childs = gameObject.GetComponentsInChildren<SpriteRenderer> ();
		if (childs.Length > 2) {
			
			if(once) {
				once = false;
				foreach(var child in childs) {
					if(child.gameObject == gameObject) {
						continue;
					}
					
					
					DestroyImmediate(child.gameObject);
				}
				
			}


			return;
		}

		
		coreDirections.Clear ();
		



		var otherAssets = Physics2D.OverlapCircleAll (new Vector2 (this.gameObject.transform.localPosition.x, 
		                                                           this.gameObject.transform.localPosition.y),
		                                              1.0f);
		
		var usedAsset = new List<GameObject> ();
		
		foreach (var asset in otherAssets) {
			if(asset.gameObject.tag == "Floor") {
				usedAsset.Add(asset.gameObject);
			}
		}
		
		for (int assetIndex = 0; assetIndex < usedAsset.Count; assetIndex++) {
			
			if(gameObject == usedAsset[assetIndex]) {
				continue;
			}
			
			if(gameObject.transform.position == usedAsset[assetIndex].transform.position) {
				continue;
			}
			
			
			for(int directionIndex = 0; directionIndex < 8; directionIndex++) {
				int x = Mathf.CeilToInt(Mathf.Sin(directionIndex * 45));
				int y = Mathf.CeilToInt(Mathf.Cos(directionIndex * 45));

				Debug.Log(directionIndex.ToString() + "x " + x);
				Debug.Log(directionIndex.ToString() + "y "+ y);


				Vector2 p1 = gameObject.transform.position;
				p1.x -= x;
				p1.y -= y;
				
				Vector2 p2 = usedAsset[assetIndex].transform.position;
				
				if(p1 == p2) {
					safeAdd(directionIndex);
					safeAdd (-1);
					
				}
			}
		}
		foreach (var direction in coreDirections) {
			int index = (int)(direction / 2);
			
			GameObject temp = null;
			
			if(direction == -1) {
				temp = GameObject.Instantiate(black) as GameObject;
				temp.GetComponent<SpriteRenderer>().sortingOrder = 3;
			} else if((direction % 2) != 0) {
				temp = GameObject.Instantiate(wall) as GameObject;
				temp.GetComponent<SpriteRenderer>().sortingOrder = 4;
			} else {
				temp = GameObject.Instantiate(corner) as GameObject;
				temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
				index--;
			}
			
			
			temp.transform.parent = gameObject.transform;
			temp.transform.localPosition = Vector3.zero;
			temp.transform.Rotate(0, 0, 90 * index);
		}
	}
}
