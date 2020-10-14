using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private Vector2 velocity;

    public float smoothTimeX;

    public GameObject player;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 6.5f, ref velocity.x, smoothTimeX);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
