using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Reset();
        //gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -7f || Input.GetButtonDown("Reset"))
        {
            Die();
        }
	}

    void FixedUpdate()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        //transform.position.Set(transform.position.x + 1f, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Reset();
            GameManager.instance.PlayerWin();

            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            //Invoke("Restart", 1f);
        }
    }

    public void resetPosition()
    {
        GetComponent<Transform>().position = new Vector3(-6.5f, -5.5f, GetComponent<Transform>().position.z);
    }

    public void Die()
    {
        Reset();
        GameManager.instance.PlayerDeath();
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        resetPosition();
        //GetComponent<MovingObject>().Grounded = false;
    }

    private void Restart()
    {
        //Load the last scene loaded, in this case Main, the only scene in the game.
        //Application.LoadLevel(Application.loadedLevel);
    }
}
