using UnityEngine;
using System.Collections;

public class SelectScript : MonoBehaviour {

    public AudioClip takeSound;
    public AudioClip cantPutSound;
    public AudioClip putSound;
	public float blinkTime = 5f;
	public string selectKey = "Fire1";

	private GameObject objectInHand;
	private float timeToDoAction;
	private string prevTag;
	private float blink;


	void Awake() {
		timeToDoAction = 0f;
	}

	void FixedUpdate() {
		if (timeToDoAction > 0) {
			timeToDoAction -= Time.deltaTime;
			return;
		}
		if (blink > 0)
			blink -= Time.deltaTime;
		if (objectInHand == null)
			return;
		objectInHand.gameObject.transform.position = GetComponentInParent<Transform> ().position;
		if (blink < 0) {
			objectInHand.GetComponent<SpriteRenderer> ().enabled = objectInHand.GetComponent<SpriteRenderer> ().enabled ? false : true;
			blink = blinkTime;
		}
        if (CanPutDown() && Input.GetButton(selectKey)) {
            SoundManager.instance.PlayAudio(putSound);
            ToggleObject();
            objectInHand = null;
        }
        else if (Input.GetButtonDown(selectKey))
        {
            SoundManager.instance.PlayAudio(cantPutSound);
        }

    }

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "GroundCheck" || col.gameObject.tag == "Player" || !CanTake ())
			return;

		objectInHand = col.gameObject;
		prevTag = objectInHand.tag;
		ToggleObject ();
        SoundManager.instance.PlayAudio(takeSound);
	}

	private bool CanTake() {
        if (timeToDoAction > 0 ||
            !Input.GetButton(selectKey) ||
            objectInHand != null)
            return false;

		return true;
	}

	private void ToggleObject() {
		if(objectInHand.GetComponent<FallScript>()) {
			objectInHand.GetComponent<FallScript>().fallingSpeed = objectInHand.GetComponent<FallScript>().fallingSpeed == 0 ? objectInHand.GetComponent<FallScript>().fallingSpeedDefault : 0f;
		}
		objectInHand.GetComponent<Collider2D> ().enabled = objectInHand.GetComponent<Collider2D> ().enabled ? false : true;
		objectInHand.GetComponent<SpriteRenderer> ().enabled = true;
		objectInHand.tag = objectInHand.tag == "Cursor" ? prevTag : "Cursor";
		GetComponent<Collider2D>().enabled = GetComponent<Collider2D>().enabled ? false : true;
		GetComponentInParent<SpriteRenderer>().enabled = GetComponentInParent<SpriteRenderer> ().enabled ? false : true;
		timeToDoAction = 0.2f;
		blink = blinkTime;
	}

    private bool CanPutDown() {
        Collider2D hitColliders = Physics2D.OverlapCircle(GetComponentInParent<Transform>().position, 0.3f, 1 << LayerMask.NameToLayer("PlayerLayer") | 1 << LayerMask.NameToLayer("BlockingLayer"));
        if (timeToDoAction > 0 || hitColliders != null)
            return false;


		return true;
	}

    public void Reset()
    {
        objectInHand = null;
        GetComponent<Collider2D>().enabled = true;
    }
}
