using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public float LifespanInSeconds = 45;
	
	public void Start() {
		StartCoroutine(Cleanup(LifespanInSeconds));
	}
	
	public void OnTriggerEnter() {
		Destroy(gameObject);
	}
	
	IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}