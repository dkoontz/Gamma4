using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public float LifespanInSeconds = 45;
	public float Damage = 40;
	
	public void Start() {
		StartCoroutine(Cleanup(LifespanInSeconds));
	}
	
	public void OnTriggerEnter(Collider other) {
		if("Weapon" == other.tag && other.transform.parent.parent.gameObject.GetComponent<Ship>().Firing) {
			Destroy(gameObject);
		}
	}
	
	public void OnCollisionEnter(Collision other) {
		if("Ship" == other.gameObject.tag) {
			other.transform.parent.gameObject.GetComponent<Ship>().Damage(Damage);
			Destroy(gameObject);
		}
	}
	
	IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}