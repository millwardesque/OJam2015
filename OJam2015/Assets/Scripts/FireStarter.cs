﻿using UnityEngine;
using System.Collections;

public class FireStarter : MonoBehaviour {

	public void LetsStartAFire() {
		FireManager.Instance.StartFire (gameObject.transform.position);
	}
}