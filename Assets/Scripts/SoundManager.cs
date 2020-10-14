using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;
    public AudioSource source;
    public AudioSource music;

    void Awake () {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        music.Play();
	}

    public void PlayAudio(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
