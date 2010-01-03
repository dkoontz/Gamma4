using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {
	public float ThrustForce = 1000.0f;
	public Ship ShipBehaviour;
	
	private bool thrust;
	
	public void Update() {
		if(Input.GetButton("Thruster")) { // && ShipBehaviour.ActivateThruster(Time.deltaTime)) {
			thrust = true;		
		}
		else {
			thrust = false;
		}
	}
	
	public void FixedUpdate () {
		if(thrust) {
			ShipBehaviour.rigidbody.AddForce(-transform.forward * ThrustForce * Time.deltaTime);
		}
	}
}