using UnityEngine;
using System.Collections;

public class QuitHandler : MonoBehaviour {

	void Update () {
		if(Input.GetButton("Quit")) {
			Application.Quit();
		}
	}
}
