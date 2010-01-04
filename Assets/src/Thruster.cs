using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {
	public float ThrustForce = 1000.0f;
	public Ship ShipBehaviour;
	public GameObject ExhaustTrail;
	
	private bool thrust;
	
	public void Start() {
		ExhaustTrail.transform.parent = gameObject.transform;
	}
	
	public void Update() {
		if(Input.GetButton("Thruster") && ShipBehaviour.ActivateThruster(Time.deltaTime)) {
			thrust = true;
			ExhaustTrail.GetComponent<ParticleEmitter>().emit = true;
		}
		else {
			thrust = false;
			ExhaustTrail.GetComponent<ParticleEmitter>().emit = false;
		}
	}
	
	public void FixedUpdate () {
		if(thrust) {
			ShipBehaviour.rigidbody.AddForce(-transform.forward * ThrustForce * Time.deltaTime);
		}
	}
}