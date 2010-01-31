using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BriefingSlideshow : MonoBehaviour {

	public List<GUITexture> screens = new List<GUITexture>();
	public List<float> pauses = new List<float>();
	
	float screenWidth = 555;
	float screenHeight = 402;
	
	public void Start () {
		StartCoroutine(SlideShow());
	}
	
	IEnumerator SlideShow () {
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
