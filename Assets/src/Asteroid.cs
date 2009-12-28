using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public void OnTriggerEnter() {
		Destroy(gameObject);
	}
}