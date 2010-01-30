using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {
	public float ThrustForce = 1000.0f;
	public GameObject ExhaustTrail;
	
	static Ship ship;
	bool thrust;
	
	public void Start() {
		if(null == ship) {
			ship = GameObject.Find("Ship").GetComponent<Ship>();
		}
		ExhaustTrail.transform.parent = gameObject.transform;
	}
	
	public void Update() {
		if(Input.GetButton("Player1") && ship.ActivateThruster(Time.deltaTime)) {
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
			ship.rigidbody.AddForce(-transform.forward * ThrustForce * Time.deltaTime);
		}
	}
}