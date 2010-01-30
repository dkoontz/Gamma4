using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public GUIText timeDisplay;
	public float gameMinutes = 4;
	public GameObject gameOverImage;
	
	public float timeRemaining;
	bool gameOver = false;
	
	public void Start() {	
		timeRemaining = 60 * gameMinutes;
		var guiTexture = gameOverImage.GetComponent<GUITexture>();
		guiTexture.color = new Color(guiTexture.color.r, 
			                             guiTexture.color.g, 
										guiTexture.color.b, 
                                       	0);
	}
	
	public void Update() {
		if(gameOver && !gameOverImage.GetComponent<Animation>().isPlaying) {
			Application.LoadLevel("menu");
		}
		
		if(timeRemaining <= 0 && !gameOver) {
			gameOver = true;
			timeRemaining = 0;
			gameOverImage.GetComponent<Animation>().Play();
		}
		
		if(timeRemaining > 0) {
			timeRemaining -= Time.deltaTime;
		}
		
		timeDisplay.text = string.Format("Seconds Remaining: {0:0.00}", timeRemaining);
	}
	
}