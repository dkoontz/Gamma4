using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float LifespanInSeconds = 5;
	public float ThrustForce = 1000;
	public float Damage = 20;
	
	static Ship ship;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		
		StartCoroutine(Cleanup(LifespanInSeconds));
	}
	
	public void OnTriggerEnter(Collider other) {
		if("Weapon" == other.tag && ship.Firing) {
			Destroy(gameObject);
		}
	}
	
	public void OnCollisionEnter(Collision other) {
		if("Ship" == other.gameObject.tag) {
			ship.Damage(Damage);
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
