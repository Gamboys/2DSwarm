using UnityEngine;
using System.Collections;

public class GlobalBots : MonoBehaviour {

	public GameObject leaderBot;
	public GameObject botPrefab;
	BotController leaderBC;

	// Settings
	static int numBots = 8;

	public static GameObject[] allBots = new GameObject[numBots];



	// Use this for initialization
	void Start () {
		BuildBotArray ();
		SetBotLeader ();
	}


	// Update is called once per frame
	void Update () {
	
	}


	// Builds the bot array
	private void 
	BuildBotArray () {
		for (int i = 0; i < numBots; i++) {
			float x = (float) i;
			Vector3 pos = new Vector3 (x, 0, 0);
			allBots [i] = (GameObject)Instantiate (botPrefab, pos, Quaternion.identity, transform);
		}
	}


	// Sets bot closest to player to be leader
	private void
	SetBotLeader () {
		leaderBot = allBots [0];
		GameObject nextBot;

		BotController nextBC;
		leaderBC = leaderBot.gameObject.GetComponent<BotController> ();
		float leaderMag = leaderBC.GetTargetMag ();
		float nextMag;
	
		for (int i = 1; i < numBots; i++) {
			nextBot = allBots [i];
			nextBC = nextBot.gameObject.GetComponent<BotController> ();
			nextMag = nextBC.GetTargetMag ();
			if (nextMag < leaderMag) {
				leaderBot = allBots [i];
				leaderBC = leaderBot.gameObject.GetComponent<BotController> ();
			}
		}
		leaderBot.gameObject.tag = "BotLead";
	}


	// Builds the bot tree
	private void
	BuildBotTree () {

	}
}
