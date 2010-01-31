using UnityEngine;
using System.Collections;

public class QuitHandler : MonoBehaviour {

	void Update () {
		if(Input.GetButton("Quit")) {
			Debug.Log("got button quit");
			Application.Quit();
		}
	}
}
