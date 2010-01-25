using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public bool FixedLifespan = true;
	public float LifespanInSeconds = 45;
	public float Damage = 40;
	
	static Ship ship;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		
		if(FixedLifespan) {
			StartCoroutine(Cleanup(LifespanInSeconds));
		}
	}
	
	public void FixedUpdate() {
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}
	
	public void OnTriggerEnter(Collider other) {
//		Debug.Log("Triggered by: " + other.gameObject.name);
		if(("Weapon" == other.tag && ship.Firing) || "Respawn Point 1" == other.tag) {
			Destroy(gameObject);
		}
	}
	
	public void OnCollisionEnter(Collision other) {
//		Debug.Log("Collided by: " + other.gameObject.name);
		if("Ship" == other.gameObject.tag) {
			ship.Damage(Damage);
		}
		Destroy(gameObject);
	}
	
	IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}