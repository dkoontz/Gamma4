using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public bool FixedLifespan = true;
	public float LifespanInSeconds = 45;
	public float Damage = 40;
	
	private static Ship ship;
	
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
	
	IEnumerator Cleanup(float timeToLive) {
		yield return new WaitForSeconds(timeToLive);
		Destroy(gameObject);
	}
}