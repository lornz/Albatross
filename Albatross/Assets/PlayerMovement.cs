using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public void ballMovement() {
		float hMovement = Input.GetAxis ("Horizontal");
		float vMovement = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (hMovement, 0.0f, vMovement);
		rigidbody.AddForce (movement);
	}
}
