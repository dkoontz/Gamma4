using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		if("Ship" == other.tag) {
			other.gameObject.GetComponent<Ship>().SetNextRespawn(this.gameObject);
		}
	}
}
