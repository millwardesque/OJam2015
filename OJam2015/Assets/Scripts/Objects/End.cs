using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	public GameObject endPoint;
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if(collider2D.tag == "Player") {

			
			int endTime = ScoreTime.EndTime();

			BrainCloudUnity.BrainCloudLogic.PostScore("Time", endTime);

			GameManager.Instance.OnWin();

		}
	}
}
