using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {
	
	public float cooldown = 0.1f;

	private float canMove;
	// Use this for initialization
	void Awake () {
		canMove = cooldown;
		transform.position = new Vector2 (0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if( canMove > float.Epsilon )
			canMove -= Time.deltaTime;

		if (canMove <= 0) {
			float dist = (transform.position - Camera.main.transform.position).z;
			
			float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
			float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
			float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
			float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

			float inputX = Input.GetAxisRaw("Horizontal");
			float inputY = Input.GetAxisRaw("Vertical");

			if(inputY != 0) {
				if(transform.position.y + inputY <= topBorder-1f && transform.position.y + inputY >= bottomBorder)
					transform.Translate (new Vector2 (0f, inputY));
			}
			if(inputX != 0) {
				if(transform.position.x + inputX <= rightBorder && transform.position.x + inputX >= leftBorder)
					transform.Translate (new Vector2 (inputX, 0f));
			}
			/*
			if (Input.GetKey (KeyCode.UpArrow)) {
				if(transform.position.y + 1f <= topBorder-1f)
					transform.Translate (new Vector2 (0f, 1f));
			}
			else if (Input.GetKey (KeyCode.DownArrow)) {
				if(transform.position.y - 1f >= bottomBorder)
					transform.Translate (new Vector2 (0f, -1f));
			}
			else if (Input.GetKey (KeyCode.RightArrow)) {
				if(transform.position.x + 1f <= rightBorder)
					transform.Translate (new Vector2 (1f, 0f));
			}
			else if (Input.GetKey (KeyCode.LeftArrow )) {
				if(transform.position.x - 1f >= leftBorder)
					transform.Translate (new Vector2 (-1f, 0f));
			}
			*/
			if(transform.position.x-0.5f < leftBorder)
				transform.Translate (new Vector2 (1f, 0f));
			if(transform.position.y+0.5f > topBorder)
				transform.Translate (new Vector2 (0f, 1f));
			if(transform.position.y-0.5f < bottomBorder)
				transform.Translate (new Vector2 (0f, -1f));

			canMove = cooldown;
		}
	}
		

    public void Reset()
    {
        transform.position = new Vector2(0.5f, 0.5f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponentInChildren<SelectScript>().Reset();
    }
}
