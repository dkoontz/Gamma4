using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject player1Text;
	public GameObject player2Text;
	public GameObject player3Text;
	public GameObject player4Text;
	
	public AudioClip crossoutClip;
	public AudioClip confirmClip;
	
	bool player1Ready = false;
	bool player2Ready = false;
	bool player3Ready = false;
	bool player4Ready = false;
	
	public void Start() {
		SetGUITextureAlphaTo(player1Text, 0);
		SetGUITextureAlphaTo(player2Text, 0);
		SetGUITextureAlphaTo(player3Text, 0);
		SetGUITextureAlphaTo(player4Text, 0);
	}
	
	public void Update () {
		if(Input.GetButton("Player1")) {
			if(!player1Ready) {
				player1Text.GetComponent<Animation>().Stop();
				SetGUITextureAlphaTo(player1Text, 1);
				player1Ready = true;
				PlayConfirmationSound();
			}
		}

		if(Input.GetButton("Player2")) {
			if(!player2Ready) {
				player2Text.GetComponent<Animation>().Stop();
				SetGUITextureAlphaTo(player2Text, 1);
				player2Ready = true;
				PlayConfirmationSound();
			}
		}
		
		if(Input.GetButton("Player3")) {
			if(!player3Ready) {
				player3Text.GetComponent<Animation>().Stop();
				SetGUITextureAlphaTo(player3Text, 1);
				player3Ready = true;
				PlayConfirmationSound();
			}
		}
		
		if(Input.GetButton("Player4")) {
			if(!player4Ready) {
				player4Text.GetComponent<Animation>().Stop();
				SetGUITextureAlphaTo(player4Text, 1);
				player4Ready = true;
				PlayConfirmationSound();
			}
		}
		
		if(player1Ready && player2Ready && player3Ready && player4Ready) {
			Application.LoadLevel("briefing");
		}
	}
	
	public void EnableMenu() {
		if(!player1Ready) { 
			player1Text.GetComponent<Animation>().Play();
		}
		if(!player2Ready) {
			player2Text.GetComponent<Animation>().Play();
		}
		if(!player3Ready) {
			player3Text.GetComponent<Animation>().Play();
		}
		if(!player4Ready) {
			player4Text.GetComponent<Animation>().Play();
		}
	}
	
	public void PlayCrossoutSound() {
		GetComponent<AudioSource>().clip = crossoutClip;
		GetComponent<AudioSource>().Play();
	}
	
	public void PlayConfirmationSound() {
		GetComponent<AudioSource>().clip = confirmClip;
		GetComponent<AudioSource>().Play();
	}
	
	private void SetGUITextureAlphaTo(GameObject target, float alpha) {
		var guiTexture = target.GetComponent<GUITexture>();
		guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, alpha);
	}
}
