using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour {

	public int songNumber;
	public float fadeTime;
	public bool stopMusic;

	private MusicPlayer musicPlayer;

	// Use this for initialization
	void Start () {
		musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
		if (stopMusic) {
			musicPlayer.StopMusic(fadeTime);
		}
		else {
			musicPlayer.ChangeMusic(songNumber, fadeTime);
		}
	}
}
