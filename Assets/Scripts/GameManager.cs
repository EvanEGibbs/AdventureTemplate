using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float fadeInTime = 0;
	public float fadeOutTime = 0;

	private bool startFadeOut = false;
	private Image fadePanel;
	private Color currentColor = Color.black;
	private string newScene = "";

	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image>();
		if (fadeInTime == 0) {
			currentColor.a = 0;
			fadePanel.color = currentColor;
		} else if (fadeInTime > 0) {
			currentColor = Color.black;
			fadePanel.color = currentColor;
		} else {
			Debug.LogError("Fade In Time Needs To Be 0 or Greater");
		}
	}

	// Update is called once per frame
	void Update() {
		if (fadeInTime > 0) {
			if (Time.timeSinceLevelLoad < fadeInTime) {
				float alphaChange = Time.deltaTime / fadeInTime;
				currentColor.a -= alphaChange;
				if (currentColor.a < 0) {
					currentColor.a = 0;
					fadeInTime = 0;
				}
				fadePanel.color = currentColor;
			}
		}
		if (startFadeOut) {
			float alphaChange = Time.deltaTime / fadeOutTime;
			currentColor.a += alphaChange;
			if (currentColor.a > 1) {
				currentColor.a = 1;
				EnterNewScene();
			}
			fadePanel.color = currentColor;
		}
	}

	public void ChangeScene(string name) {
		newScene = name;
		if (fadeOutTime == 0f) {
			SceneManager.LoadScene(newScene);
		} else if (fadeOutTime > 0) {
			startFadeOut = true;
		} else if (fadeOutTime < 0) {
			Debug.LogError("fade time needs to be greater than 0!");
		}
	}
	void EnterNewScene() {
		SceneManager.LoadScene(newScene);
	}
}
