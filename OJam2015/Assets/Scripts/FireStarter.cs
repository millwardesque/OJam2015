using UnityEngine;
using System.Collections;

public class FireStarter : MonoBehaviour {
	public float fireSpawnProbability = 0.01f;

	public void LetsStartAFire() {
		FireManager.Instance.StartFire (gameObject.transform.position, fireSpawnProbability);
	}
}
