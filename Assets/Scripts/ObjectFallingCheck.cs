using UnityEngine;
using System.Collections;

public class ObjectFallingCheck : MonoBehaviour {
	// Update is called once per frame
	void FixedUpdate () {
		Collider2D hitColliders = Physics2D.OverlapCircle (GetComponent<Transform> ().position, 0.2f, 1 << LayerMask.NameToLayer ("BlockingLayer"));
		if (hitColliders != null) {
            if (hitColliders.gameObject.tag == "Physics")
                GetComponentInParent<PlayerScript>().Die();
		}
	}
}
