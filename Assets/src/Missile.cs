using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float LifespanInSeconds = 5;
	public float ThrustForce = 1000;
	public float Damage = 20;
	
	public void Start () {
		StartCoroutine(Cleanup(LifespanInSeconds));
	}
	
	public void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);
		if("Weapon" == other.tag && other.transform.parent.parent.gameObject.GetComponent<Ship>().Firing) {
			Destroy(gameObject);
		}
	}
	
	public void OnCollisionEnter(Collision other) {
		if("Ship" == other.gameObject.tag) {
			other.gameObject.GetComponent<Ship>().Damage(Damage);
		}
		Destroy(gameObject);
	}
	
	public void Update() {
		Debug.DrawRay(transform.position, transform.forward);
	}
	
	public void Launch() {
		rigidbody.AddForce(transform.forward * ThrustForce);
	}
	
	IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}
