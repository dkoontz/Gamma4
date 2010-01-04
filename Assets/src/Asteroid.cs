using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public float LifespanInSeconds = 45;
	public float Damage = 50;
	
	public void Start() {
		StartCoroutine(Cleanup(LifespanInSeconds));
	}
	
	public void OnTriggerEnter(Collider other) {
		if("Ship" == other.tag) {
			other.GetComponent<Ship>().Damage(Damage);
		}
		Destroy(gameObject);
	}
	
	IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}