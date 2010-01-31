using UnityEngine;
using System.Collections;

public class WarpGate : MonoBehaviour {

	public Game game;
	
	GameObject ship;
	
	public void OnTriggerEnter(Collider other) {
		if("Ship" == other.tag) {
			ship = other.gameObject;
			game.Win();
			ship.rigidbody.AddForce(transform.forward * 50000);
		}
	}
	
	public void Update() {
		
	}
}
