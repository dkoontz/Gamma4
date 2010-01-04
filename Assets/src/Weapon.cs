using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject Missile;
	public Ship ShipBehaviour;
	
	public void Update() {
		if(Input.GetButtonDown("Weapon") && ShipBehaviour.ActivateWeapon()) {
			var missile = (GameObject)Instantiate(Missile);
			missile.transform.rotation = transform.rotation;
			missile.transform.position = ShipBehaviour.transform.position + (transform.forward * 12);
			missile.transform.Rotate(new Vector3(90, 0, 0));
			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
			missile.transform.rigidbody.angularVelocity += ShipBehaviour.rigidbody.angularVelocity;

			missile = (GameObject)Instantiate(Missile);
			missile.transform.rotation = transform.rotation;
			missile.transform.Rotate(new Vector3(0, 3, 0));
			missile.transform.position = ShipBehaviour.transform.position + (missile.transform.forward * 12);
			missile.transform.Rotate(new Vector3(90, 0, 0));
			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
			missile.transform.rigidbody.angularVelocity += ShipBehaviour.rigidbody.angularVelocity;
			
			missile = (GameObject)Instantiate(Missile);
			missile.transform.rotation = transform.rotation;
			missile.transform.Rotate(new Vector3(0, -3, 0));
			missile.transform.position = ShipBehaviour.transform.position + (missile.transform.forward * 12);
			missile.transform.Rotate(new Vector3(90, 0, 0));
			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
			missile.transform.rigidbody.angularVelocity += ShipBehaviour.rigidbody.angularVelocity;
		}
	}
}