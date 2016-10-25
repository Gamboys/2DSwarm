using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float thrustSpeed;
	public float rotateSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();

		// Movement input
		rb2d.angularVelocity = Input.GetAxis("Horizontal") * rotateSpeed;
		rb2d.velocity = transform.up * thrustSpeed * Input.GetAxis ("Vertical");
	}
}
