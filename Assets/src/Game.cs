using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public GUIText timeDisplay;
	public float gameMinutes = 4;
	public GameObject gameLostImage;
	public GameObject gameWonImage;
	public float animationDuration = 5;
	public AudioClip gameWonClip;
	public AudioClip gameLostClip;
	
	float gameOverScreenPercentage = 1.678329f;
	float timeRemaining;
	bool gameLost = false;
	bool gameWon = false;
	bool animating = false;
	
	public void Start() {	
		timeRemaining = 60 * gameMinutes;
		var guiTexture = gameLostImage.GetComponent<GUITexture>();
		guiTexture.color = new Color(guiTexture.color.r, 
			                             guiTexture.color.g, 
										guiTexture.color.b, 
                                       	0);
		guiTexture = gameWonImage.GetComponent<GUITexture>();
		guiTexture.color = new Color(guiTexture.color.r, 
			                             guiTexture.color.g, 
										guiTexture.color.b, 
                                       	0);
	}
	
	public void Update() {
		if(gameLost && !animating) {
			Application.LoadLevel("menu");
		}
		
		if(gameWon && !animating) {
			Application.LoadLevel("menu");
		}
		
		if(timeRemaining <= 0 && !gameLost && !gameWon) {
			gameLost = true;
			timeRemaining = 0;
			var audio = GetComponent<AudioSource>();
			audio.clip = gameLostClip;
			audio.Play();
			var imageScale = GameOverImageScale(gameLostImage);
			gameLostImage.transform.localScale = new Vector3(imageScale, imageScale, imageScale);
			Animate(gameLostImage);
		}
		
		if(timeRemaining > 0) {
			timeRemaining -= Time.deltaTime;
		}
		
		timeDisplay.text = string.Format("Seconds Remaining: {0:0.00}", timeRemaining);
	}
	
	public void Win() {
		if(!gameWon && !gameLost) {
			gameWon = true;
			var audio = GetComponent<AudioSource>();
			audio.clip = gameWonClip;
			audio.Play();
			
			var imageScale = GameOverImageScale(gameWonImage);
			gameWonImage.transform.localScale = new Vector3(imageScale, imageScale, imageScale);
			Animate(gameWonImage);
		}
	}
	
	float GameOverImageScale(GameObject image) {
		return ((gameOverScreenPercentage * Screen.width) / image.GetComponent<GUITexture>().texture.width) - 1.0f;
	}
	
	void Animate(GameObject image) {
		StartCoroutine(SlerpAlphaAndScale(image));
	}
	
	IEnumerator SlerpAlphaAndScale(GameObject image) {
		animating = true;
		var startScale = image.transform.localScale;
		var texture = image.GetComponent<GUITexture>();
		var startTime = Time.time;
		
		while(Time.time - startTime < animationDuration) {
			yield return new WaitForSeconds(0.001f);
			var percentComplete = (Time.time - startTime) / animationDuration;
			image.transform.localScale = Vector3.Slerp(startScale + Vector3.one, startScale, percentComplete);
			texture.color = new Color(texture.color.r, 
									  texture.color.g, 
									  texture.color.b, 
									  Mathf.Lerp(0, 1, percentComplete));
		}
		yield return new WaitForSeconds(2);
		
		animating = false;
	}
}