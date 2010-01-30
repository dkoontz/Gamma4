using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject player1Text;
	public GameObject player2Text;
	public GameObject player3Text;
	public GameObject player4Text;
	
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
			player1Text.GetComponent<Animation>().Stop();
			SetGUITextureAlphaTo(player1Text, 0.5f);
			player1Ready = true;
		}

		if(Input.GetButton("Player2")) {
			player2Text.GetComponent<Animation>().Stop();
			SetGUITextureAlphaTo(player2Text, 0.5f);
			player2Ready = true;
		}
		
		if(Input.GetButton("Player3")) {
			player3Text.GetComponent<Animation>().Stop();
			SetGUITextureAlphaTo(player3Text, 0.5f);
			player3Ready = true;
		}
		
		if(Input.GetButton("Player4")) {
			player4Text.GetComponent<Animation>().Stop();
			SetGUITextureAlphaTo(player3Text, 0.5f);
			player4Ready = true;
		}
		
		if(player1Ready && player2Ready && player3Ready && player4Ready) {
			Application.LoadLevel("main_game");
		}
	}
	
	public void EnableMenu() {
		player1Text.GetComponent<Animation>().Play();
		player2Text.GetComponent<Animation>().Play();
		player3Text.GetComponent<Animation>().Play();
		player4Text.GetComponent<Animation>().Play();
	}
	
	private void SetGUITextureAlphaTo(GameObject target, float alpha) {
		var guiTexture = target.GetComponent<GUITexture>();
		guiTexture.color = new Color(guiTexture.color.r, guiTexture.color.g, guiTexture.color.b, alpha);
	}
}
