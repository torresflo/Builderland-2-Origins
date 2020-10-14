using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public float speed = 1f;
	public float speedFall = 10f;

	private Rigidbody2D rb2d;
	private Vector2 movement;
	private bool grounded;


	// Use this for initialization
	void Start () {
		grounded = false;
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (grounded)
			movement = new Vector2 (speed, 0f);
		else
			movement = new Vector2 (0f, -speedFall);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb2d.velocity = movement;
	}

	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	public bool Grounded {
		get { return grounded; }
		set { grounded = value; }
	}
}
