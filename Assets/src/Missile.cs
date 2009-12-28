using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float LifespanInSeconds = 5;
	public float ThrustForce = 1000;
	
	public void Start () {
		StartCoroutine(Cleanup(LifespanInSeconds));
		rigidbody.AddForce(transform.up * ThrustForce);
	}
	
	public void OnTriggerEnter() {
		Destroy(gameObject);
	}
	
	private IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}
