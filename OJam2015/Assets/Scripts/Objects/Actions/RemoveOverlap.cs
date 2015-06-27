using UnityEngine;

public class RemoveOverlap : MonoBehaviour {
	public void OnGui() {
		DestroyOverlappedAssets ();
	}

	void DestroyOverlappedAssets () {
		var otherAssets = Physics2D.OverlapCircleAll (new Vector2 (this.gameObject.transform.localPosition.x, 
		                                                           this.gameObject.transform.localPosition.y),
		                                              0.1f);
		var distance = Mathf.Infinity;
		foreach (var asset in otherAssets) {
			if (asset.transform.parent != null && asset.transform.parent.name == this.gameObject.transform.parent.name) {
				if (asset.gameObject != this.gameObject) {
					DestroyImmediate (asset.gameObject);
				}
			}
		}
	}
}
