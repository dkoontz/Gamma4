using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject Missile;
	public Ship ShipBehaviour;
	
	public void Update() {
		if(Input.GetButtonDown("Weapon") && ShipBehaviour.ActivateWeapon()) {
			GameObject missile = (GameObject)Instantiate(Missile);
			missile.transform.rigidbody.angularVelocity += transform.rigidbody.angularVelocity;
			missile.transform.position = transform.position + (transform.up * 2);
			missile.transform.rotation = transform.rotation;
			
			// apply reaction force from Missile for a little "kickback"
//			transform.rigidbody.AddForce(transform.up * -missile.GetComponent<Missile>().ThrustForce);
		}
	}
}