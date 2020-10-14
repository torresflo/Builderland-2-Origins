using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {
    public AudioSource startSound;
    protected bool menuActive = false;

	// Use this for initialization
	void Start () {
        GameObject.Find("controls").GetComponent<SpriteRenderer>().enabled = false;
        Invoke("SplashScreen", 3f);
        Invoke("blink", 3f);
        Screen.SetResolution(512,512, false);
    }
	
	protected void SplashScreen()
    {
        GameObject.Find("splash_screen").SetActive(false);
        menuActive = true;
    }

    void Update()
    {
        if (menuActive)
            if (Input.GetButton("Fire1"))
            {
                menuActive = false;
                GameObject.Find("controls").GetComponent<SpriteRenderer>().enabled = true;
                startSound.Play();
                Invoke("startGame", 3f);
            }
    }

    void blink()
    {
        GameObject.Find("blinker").GetComponent<SpriteRenderer>().enabled = !GameObject.Find("blinker").GetComponent<SpriteRenderer>().enabled;
        Invoke("blink", 0.7f);
    }

    void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
