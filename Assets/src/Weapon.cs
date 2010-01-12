using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public Ship ShipBehaviour;
	public GameObject Laser;
	
	public void Start() {
		Laser.transform.parent = gameObject.transform;
		Laser.GetComponent<MeshRenderer>().enabled = false;
		Laser.GetComponent<CapsuleCollider>().isTrigger = true;
	}
	
	public void Update() {
		if(Input.GetButton("Weapon") && ShipBehaviour.ActivateWeapon(Time.deltaTime)) {
			Laser.GetComponent<MeshRenderer>().enabled = true;
			ShipBehaviour.Firing = true;
			
			
			
//			var missile = (GameObject)Instantiate(Missile);
//			missile.transform.rotation = transform.rotation;
//			missile.transform.position = ShipBehaviour.transform.position + (transform.forward * 12);
//			missile.transform.Rotate(new Vector3(90, 0, 0));
//			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
//			missile.transform.rigidbody.angularVelocity += ShipBehaviour.rigidbody.angularVelocity;
//
//			missile = (GameObject)Instantiate(Missile);
//			missile.transform.rotation = transform.rotation;
//			missile.transform.Rotate(new Vector3(0, 3, 0));
//			missile.transform.position = ShipBehaviour.transform.position + (missile.transform.forward * 12);
//			missile.transform.Rotate(new Vector3(90, 0, 0));
//			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
//			missile.transform.rigidbody.angularVelocity += ShipBehaviour.rigidbody.angularVelocity;
//			
//			missile = (GameObject)Instantiate(Missile);
//			missile.transform.rotation = transform.rotation;
//			missile.transform.Rotate(new Vector3(0, -3, 0));
//			missile.transform.position = ShipBehaviour.transform.position + (missile.transform.forward * 12);
//			missile.transform.Rotate(new Vector3(90, 0, 0));
//			missile.rigidbody.AddForce(missile.transform.up * missile.GetComponent<Missile>().ThrustForce);
//			missile.transform.rigidbody.angularVelocity += ShipBehaviour.rigidbody.angularVelocity;
		}
		else {
			Laser.GetComponent<MeshRenderer>().enabled = false;
			ShipBehaviour.Firing = false;
		}
	}
}