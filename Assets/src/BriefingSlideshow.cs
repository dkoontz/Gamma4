using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BriefingSlideshow : MonoBehaviour {

	public List<GUITexture> screens = new List<GUITexture>();
	public List<float> pauses = new List<float>();
	public GUIText skipNotice;
	public float skipNoticeDisplayDuration = 2f;
	
	float screenWidth = 555;
	float screenHeight = 402;
	
	public void Start () {
		StartCoroutine(SlideShow());
	}
	
	public void Update() {
		if(Input.GetButton("Player1") && Input.GetButton("Player2") && Input.GetButton("Player3") && Input.GetButton("Player4")) {
			Application.LoadLevel("main_game");	
		}
	}
	
	IEnumerator SlideShow () {
		yield return new WaitForSeconds(skipNoticeDisplayDuration);
		skipNotice.enabled = false;
		
		var widthScale = (1 - (screenWidth / Screen.width)) * 0.8f;
		var heightScale = (1 - (screenHeight / Screen.height)) * 0.8f;
		for(var index = 0; index < screens.Count; index++) {
			screens[index].transform.localScale = new Vector3(widthScale, heightScale, 1);
			screens[index].transform.Translate(0, 0, index * 0.1f);
			yield return new WaitForSeconds(pauses[index]);
		}
		Application.LoadLevel("main_game");
	}
}
