using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip[] musicList;

	static MusicPlayer instance = null;

	private AudioSource audioSource;
	private bool startFadeIn = false;
	private float fadingTime = 0;
	private float fadeInTime = 0;
	private int newSongNumber = 0;
	private bool stopMusic = false;
	private float stopFade = 0;

	void Awake (){
		if (instance != null){
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update() {
		if (fadingTime > 0) {
			audioSource.volume -= Time.deltaTime / fadingTime;
			if (audioSource.volume <= 0) {
				fadingTime = 0;
				audioSource.clip = musicList[newSongNumber];
				audioSource.Play();
				audioSource.volume = 0;
				startFadeIn = true;
			}
		}
		if (startFadeIn == true) {
			audioSource.volume += Time.deltaTime / fadeInTime;
			if (audioSource.volume >= 1) {
				audioSource.volume = 1;
				startFadeIn = false;
			}
		}
		if (stopMusic) {
			audioSource.volume -= Time.deltaTime / stopFade;
			if (audioSource.volume <= 0) {
				audioSource.volume = 0;
				stopMusic = false;
			}
		}
	}

	public void ChangeMusic(int songNumber, float fadeTime) {
		startFadeIn = false;
		stopMusic = false;
		newSongNumber = songNumber;
		if (fadeTime == 0) {
			fadingTime = 0;
			audioSource.clip = musicList[newSongNumber];
			audioSource.volume = 1;
			audioSource.Play();
		} else if (fadeTime > 0) {
			fadingTime = fadeTime;
			fadeInTime = fadeTime;
		}
	}

	public void StopMusic(float afadeTime) {
		fadingTime = 0;
		startFadeIn = false;
		if (afadeTime == 0) {
			audioSource.Stop();
		}
		else {
			stopFade = afadeTime;
			stopMusic = true;
		}
	}
}
