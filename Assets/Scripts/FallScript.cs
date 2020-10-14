using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {

	[HideInInspector] public float fallingSpeed;
	public float fallingSpeedDefault = 15f;

	void Awake() {
		fallingSpeed = fallingSpeedDefault;
	}
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -fallingSpeed);
	}
}
