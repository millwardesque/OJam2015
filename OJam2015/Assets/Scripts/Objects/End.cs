using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	public GameObject endPoint;

	public GameObject leaderboard = null;
	
	void OnTriggerEnter2D(Collider2D collider2D) {
		if(collider2D.tag == "Player") {

			var leaderBoard = GameObject.Instantiate(leaderboard);

			leaderBoard.transform.parent = CanvasInstance.instance.gameObject.transform;
			 

			var children = CanvasInstance.instance.gameObject.GetComponentsInChildren<HealthBarInstance>();
			foreach(var child in children) {
				child.gameObject.SetActive(false);
			}

			int endTime = ScoreTime.EndTime();

			BrainCloudUnity.BrainCloudLogic.PostScore("Time", endTime);

			GameManager.Instance.OnWin();

		}
	}
}
