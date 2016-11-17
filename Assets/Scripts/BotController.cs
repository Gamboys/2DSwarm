using UnityEngine;
using System.Collections;

// TODO: Add idle behaviour (move randomly)
// TODO: Make bots follow nearby bots rather than a single leader
// TODO: Fix up target system, targetMag or seperate and explicit (figure out CalcTarger ())

public class BotController : MonoBehaviour {

	// GameObjects
	GameObject target;
	GameObject player;
	GameObject botMaster;
	GlobalBots globalBots;
	Rigidbody2D rb2d;

	// Vectors
	public Vector2 toTarget;
	public Vector2 toPlayer;
	float targetMag;

	// Settings
	public float speedUpper;
	public float speedLower;
	public float speed;
	static float spaceAllowance = 1.4f;


	/*------------------------------------------------------------------------*/


	void
	Start () {
		// Initialise GameObject shit
		rb2d = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
		botMaster = GameObject.Find ("BotMaster");
		globalBots = botMaster.gameObject.GetComponent<GlobalBots> ();

		SetTarget ();
		// speed = CalcSpeed ();
	}


	void
	FixedUpdate () {
		CalcToTarget ("player");
		CalcToTarget ("target");
		if (toTarget.magnitude > 2) {
			MoveToTarget ();
		} else {
			MoveBack ();
		}
	}

	/*------------------------------------------------------------------------*/

	public float
	GetTargetMag () {
		return targetMag;
	}


	private void
	MoveBack () {
		if (toTarget.magnitude < 1.75) {
			rb2d.velocity = -toTarget * 4 * speed;
		} else {
			rb2d.velocity = Vector2.zero;
		}
	}

	// TODO: Add functionality to follow nearest bot
	private void
	SetTarget () {
		target = GameObject.Find ("Player");
		targetMag = CalcTargetMag ();
	}


	// Finds a bot that is closer to player and closest to the current bot instance
	private void
	CalcTarget () {

		BotController candidateBC = GetComponent<BotController> ();
		GameObject[] botsArray = GlobalBots.allBots;
		foreach (GameObject bot in botsArray) {
			BotController otherBC = bot.GetComponent<BotController> ();
			if (toPlayer.magnitude > otherBC.toPlayer.magnitude
				&& candidateBC.toPlayer.magnitude > otherBC.toPlayer.magnitude) {
				candidateBC = bot.GetComponent<BotController> ();
			}
		}
		target = candidateBC.gameObject;
	}

	private float
	CalcTargetMag () {
		return toTarget.magnitude;
	}


	private void
	CalcToTarget (string input) {
		if (input == "target") {
			Vector2 target2D = (Vector2)target.gameObject.transform.position;
			toTarget = target2D - (Vector2)transform.position;
		} else if (input == "player") {
			Vector2 target2D = (Vector2)player.gameObject.transform.position;
			toPlayer = target2D - (Vector2)transform.position;
		}
	}


	// Moves towards target
	// TODO: Add rotation towards target
	private void
	MoveToTarget () {
		
		if (toTarget.magnitude > spaceAllowance) {
			rb2d.velocity = toTarget * speed;
		} else {
			
			rb2d.velocity = Vector2.zero;
		}
	}


	private float
	CalcSpeed () {
		return Random.Range (speedLower, speedUpper);
	}


	/* TODO: Quaternion rotation. Iterate from 0 -> 2(pi) and multiply by magic number
	then set quaternion rotation to said product (don't add force, set it). But how to
	set average direction?
	*/
	private void
	SinRotate () {

	}

}
	
