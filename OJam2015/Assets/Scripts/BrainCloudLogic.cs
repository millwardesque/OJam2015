using UnityEngine;
using System.Collections;
using BrainCloudUnity.HUD;
using System.Collections.Generic;
using LitJson;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BrainCloudUnity
{
	#if UNITY_EDITOR
	[InitializeOnLoad]
	#endif
	
	public class BrainCloudLogic : MonoBehaviour
	{
		private string m_username = "";
		private string m_password = "";
		
		private Vector2 m_scrollPosition;
		private string m_authStatus = "Welcome to brainCloud";
		
		void Start()
		{

			if (instance && instance.gameObject) {
				Destroy(gameObject);
				return;
			}
			
			instance = this;

			///////////////////////////////////////////////////////////////////
			// brainCloud game configuration
			///////////////////////////////////////////////////////////////////
			
			BrainCloudWrapper.Initialize();
			
			///////////////////////////////////////////////////////////////////

			string playerName = "";

			int rand = Random.Range (0, 9);

			string[] playerNames = {
				"Player 1",
				"Player 2",
				"Player 3",
				"Player 4",
				"Player 5",
				"Player 6",
				"Player 7",
				"Player 8",
				"Player 9",
				"Player 10"
			};

			playerName = playerNames[rand];
			PlayerPrefs.SetString ("username", playerName);


			m_username = PlayerPrefs.GetString("username");
			PlayerPrefs.SetString ("password", "password");
			m_password = PlayerPrefs.GetString("password");

			BrainCloudWrapper.GetInstance().AuthenticateUniversal(m_username, m_password, true, OnSuccess_Authenticate, OnError_Authenticate);
		}

		private static BrainCloudLogic instance = null;


		public void OnSuccess_Authenticate(string responseData, object cbObject)
		{
			RetrieveLeaderboard (m_lbId);
		}
		
		public void OnError_Authenticate(int statusCode, int reasonCode, string statusMessage, object cbObject)
		{
		}


		class LBEntry
		{
			public string playerId;
			public string name;
			public long rank;
			public long score;
		}

		List<LBEntry> m_lb = new List<LBEntry>();
		string m_lbId = "Time";
		string m_score = "1000";
		bool m_showPlayerIds = false;

		
		public static void RetrieveLeaderboard(string leadboardId)
		{
			if (instance == null) {
				return;
			}

			instance.m_lb.Clear ();
			
			BrainCloudWrapper.GetBC ().SocialLeaderboardService.GetGlobalLeaderboard(
				leadboardId, BrainCloud.BrainCloudSocialLeaderboard.FetchType.HIGHEST_RANKED, 100,
				ReadLeaderboardSuccess, ReadLeaderboardFailure);
		}
		
		public static void PostScore(string lbId, long score)
		{
			if (instance == null) {
				return;
			}

			BrainCloudWrapper.GetBC ().SocialLeaderboardService.PostScoreToLeaderboard(
				lbId, score, null, PostScoreSuccess, PostScoreFailure);
		}
		
		public static void PostScoreSuccess(string json, object cb)
		{
			if (instance == null) {
				return;
			}

			RetrieveLeaderboard(instance.m_lbId);
		}
		
		public static void PostScoreFailure(int statusCode, int reasonCode, string statusMessage, object cb)
		{
			if (instance == null) {
				return;
			}

		}
		
		public static void ReadLeaderboardSuccess(string json, object cb)
		{
			if (instance == null) {
				return;
			}

			JsonData jObj = JsonMapper.ToObject(json);
			JsonData jLeaderboard = jObj["data"]["social_leaderboard"];
			IList entries = jLeaderboard as IList;

			LeaderboardScores.GetText ().text = "Leaderboard: \n";

			int count = 1;

			if (entries != null)
			{
				foreach (JsonData jEntry in entries)
				{
					LBEntry lbe = new LBEntry();
					lbe.playerId = (string) jEntry["playerId"];
					lbe.name = (string) jEntry["name"];
					
					if (jEntry["rank"].IsInt)
					{
						lbe.rank = (int) jEntry["rank"];
					}
					else
					{
						lbe.rank = (long) jEntry["rank"];
					}
					
					if (jEntry["score"].IsInt)
					{
						lbe.score = (int) jEntry["score"];
					}
					else
					{
						lbe.score = (long) jEntry["score"];
					}
					instance.m_lb.Add (lbe);

					LeaderboardScores.GetText ().text += lbe.rank.ToString() + ". " + lbe.score.ToString() + "\n";
					count++;

				}



				LeaderboardScores.GetText().rectTransform.sizeDelta = new Vector2(240,
				                                                                  100 * count);

				LeaderboardScores.GetImage().rectTransform.sizeDelta = new Vector2(240,
				                                                                  100 * (count + 3));

				LeaderboardScores.GetText().rectTransform.anchoredPosition = new Vector2(-120,-30);
				LeaderboardScores.GetImage().rectTransform.anchoredPosition = new Vector2(0,10);



			}
		}
		
		public static void ReadLeaderboardFailure(int statusCode, int reasonCode, string statusMessage, object cb)
		{
			if (instance == null) {
				return;
			}
		}

		[SerializeField]
		private string m_authSuccessLevel = "";
		public string AuthSuccessLevel
		{
			get {return m_authSuccessLevel;}
			set
			{
				if (m_authSuccessLevel != value)
				{
					m_authSuccessLevel = value;
					#if UNITY_EDITOR
					EditorUtility.SetDirty(this);
					#endif
				}
			}
		}
	}
}