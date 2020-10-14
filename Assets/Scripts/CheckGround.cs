using UnityEngine;
using System.Collections.Generic;

public class CheckGround : MonoBehaviour {
	
	private MovingObject player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<MovingObject> ();
	}

	void FixedUpdate() {
		Collider2D hitColliders = Physics2D.OverlapCircle (GetComponentInParent<Transform> ().position, 0.3f, 1 << LayerMask.NameToLayer("BlockingLayer"));

		if (hitColliders != null)
			player.Grounded = true;
		else
			player.Grounded = false;

        GetComponentInParent<Animator>().SetBool("falling", !player.Grounded);
	}
}
