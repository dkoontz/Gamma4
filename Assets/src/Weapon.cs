using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject Missile;
	public Ship ShipBehaviour;
	
	public void Update() {
		if(Input.GetButtonDown("Weapon") && ShipBehaviour.ActivateWeapon()) {
			var missile = (GameObject)Instantiate(Missile);
			missile.transform.rotation = transform.rotation;
			missile.transform.position = transform.position + (missile.transform.up * 2);
			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
			missile.transform.rigidbody.angularVelocity += transform.rigidbody.angularVelocity;
			
			missile = (GameObject)Instantiate(Missile);
			missile.transform.rotation = transform.rotation;
			missile.transform.Rotate(new Vector3(15, 0, 0));
			missile.transform.position = transform.position + (missile.transform.up * 2);
			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
			missile.transform.rigidbody.angularVelocity += transform.rigidbody.angularVelocity;
			
			missile = (GameObject)Instantiate(Missile);
			missile.transform.rotation = transform.rotation;
			missile.transform.Rotate(new Vector3(-15, 0, 0));
			missile.transform.position = transform.position + (missile.transform.up * 2);
			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
			missile.transform.rigidbody.angularVelocity += transform.rigidbody.angularVelocity;
			
			// apply reaction force from Missile for a little "kickback"
//			transform.rigidbody.AddForce(transform.up * -missile.GetComponent<Missile>().ThrustForce);
		}
	}
}