using UnityEngine;
using System.Collections;

public class GatePowerCell : MonoBehaviour {

	public void OnCollisionEnter(Collision other) {
		Debug.Log("colliding");
		if("Weapon" == other.gameObject.tag) {
			Destroy(gameObject);
		}
	}
	
	public void OnTriggerEnter(Collider other) {
		Debug.Log("triggering");
		if("Weapon" == other.gameObject.tag) {
			Destroy(gameObject);
		}
	}
}
